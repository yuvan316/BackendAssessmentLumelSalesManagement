using LumelSalesManagementDomain.Domain.Interfaces;
using LumelSalesManagementDomain.Models;
using LumelSalesManagementRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementDomain.Domain.Processor
{
    public class OrderManagementProcessor : IOrderManagementProcessor
    {
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public OrderManagementProcessor(IOrderQueryRepository orderQueryRepository,
            IProductQueryRepository productQueryRepository,
            ICustomerQueryRepository customerQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository;
            _productQueryRepository = productQueryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<RevenueResponse> GetRevenueAsync(RevenueRequest request)
        {
            if (request.StartDate > request.EndDate)
                throw new ArgumentException("StartDate must be less than or equal to EndDate");

            var response = new RevenueResponse
            {
                RevenueType = request.RevenueType,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            switch (request.RevenueType)
            {
                case RevenueType.Total:
                    response.TotalRevenue = await CalculateTotalRevenueAsync(request.StartDate, request.EndDate);
                    break;

                case RevenueType.ByProduct:
                    response.Products = await CalculateRevenueByProductAsync(request.StartDate, request.EndDate);
                    break;

                case RevenueType.ByCategory:
                    response.Categories = await CalculateRevenueByCategoryAsync(request.StartDate, request.EndDate);
                    break;

                case RevenueType.ByRegion:
                    response.Regions = await CalculateRevenueByRegionAsync(request.StartDate, request.EndDate);
                    break;

                default:
                    throw new ArgumentException($"Invalid RevenueType: {request.RevenueType}");
            }

            return response;
        }

        private async Task<decimal> CalculateTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderQueryRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            
            decimal totalRevenue = 0;
            foreach (var order in orders)
            {
                var product = await _productQueryRepository.GetProductByIdAsync(order.ProductId);
                if (product != null)
                {
                    var revenue = product.UnitPrice * order.QuantitySold - (product.Discount * order.QuantitySold) + product.ShippingCost;
                    totalRevenue += revenue;
                }
            }

            return totalRevenue;
        }

        private async Task<List<RevenueByProductResponse>> CalculateRevenueByProductAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderQueryRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            var groupedByProduct = orders.GroupBy(o => o.ProductId);

            var revenueByProduct = new List<RevenueByProductResponse>();

            foreach (var productGroup in groupedByProduct)
            {
                var product = await _productQueryRepository.GetProductByIdAsync(productGroup.Key);
                if (product != null)
                {
                    decimal revenue = 0;
                    int totalQuantity = 0;

                    foreach (var order in productGroup)
                    {
                        revenue += product.UnitPrice * order.QuantitySold - (product.Discount * order.QuantitySold) + product.ShippingCost;
                        totalQuantity += order.QuantitySold;
                    }

                    revenueByProduct.Add(new RevenueByProductResponse
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Revenue = revenue,
                        QuantitySold = totalQuantity
                    });
                }
            }

            return revenueByProduct;
        }

        private async Task<List<RevenueByCategoryResponse>> CalculateRevenueByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderQueryRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            var groupedByCategory = orders.GroupBy(o => o.CategoryId);

            var revenueByCategory = new List<RevenueByCategoryResponse>();

            foreach (var categoryGroup in groupedByCategory)
            {
                var category = await _productQueryRepository.GetCategoryByCategoryIdAsync(categoryGroup.Key);
                if (category != null)
                {
                    decimal revenue = 0;
                    int totalQuantity = 0;

                    foreach (var order in categoryGroup)
                    {
                        var product = await _productQueryRepository.GetProductByIdAsync(order.ProductId);
                        if (product != null)
                        {
                            revenue += product.UnitPrice * order.QuantitySold - (product.Discount * order.QuantitySold) + product.ShippingCost;
                            totalQuantity += order.QuantitySold;
                        }
                    }

                    revenueByCategory.Add(new RevenueByCategoryResponse
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        Revenue = revenue,
                        QuantitySold = totalQuantity
                    });
                }
            }

            return revenueByCategory;
        }

        private async Task<List<RevenueByRegionResponse>> CalculateRevenueByRegionAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderQueryRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            var groupedByRegion = orders.GroupBy(o => o.Region);

            var revenueByRegion = new List<RevenueByRegionResponse>();

            foreach (var regionGroup in groupedByRegion)
            {
                decimal revenue = 0;
                int totalQuantity = 0;

                foreach (var order in regionGroup)
                {
                    var product = await _productQueryRepository.GetProductByIdAsync(order.ProductId);
                    if (product != null)
                    {
                        revenue += product.UnitPrice * order.QuantitySold - (product.Discount * order.QuantitySold) + product.ShippingCost;
                        totalQuantity += order.QuantitySold;
                    }
                }

                revenueByRegion.Add(new RevenueByRegionResponse
                {
                    Region = regionGroup.Key,
                    Revenue = revenue,
                    QuantitySold = totalQuantity
                });
            }

            return revenueByRegion;
        }
    }
}
