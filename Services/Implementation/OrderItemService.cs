using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Services.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericReopsitory<Order_item> _genericReopsitory;
        private readonly IOrderService _orderService;
        private readonly ICart_ItemsService _cartItemsService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public OrderItemService(IGenericReopsitory<Order_item> genericReopsitory, IOrderService orderService, ICart_ItemsService cartItemsService,IProductService productService,IMapper mapper)
        {
            _mapper = mapper;
            _genericReopsitory = genericReopsitory;
            _orderService = orderService;
            _cartItemsService = cartItemsService;
            _productService = productService;
        }

        public List<ViewOrderitemVM> GetALLOrder_item(string userid)
        {
            List<ViewCart_itemVM> viewCart_ItemVMs = _cartItemsService.GetCart_item(userid);
            List<ViewOrderitemVM> viewOrderitemVMs = new();
            foreach (var itemVM in viewCart_ItemVMs)
            {
                ReadProductVM readProductVM=_productService.GetReadProductVM(itemVM.ProductId);
                viewOrderitemVMs.Add(new ViewOrderitemVM()
                {
                    productname=readProductVM.ProductName,
                    quantity=itemVM.quantity,
                    basictotal=readProductVM.SellPrice*itemVM.quantity

                });
            }
            return viewOrderitemVMs;


        }
        public CreateOrderItemVM CreateOrderitem(CreateOrderItemVM createOrderItemVM)
        {
            Order_item order_Item=_mapper.Map<Order_item>(createOrderItemVM);
            order_Item=_genericReopsitory.Add(order_Item);
            CreateOrderItemVM createOrderItem=_mapper.Map<CreateOrderItemVM>(order_Item);
            return createOrderItem;
        }
        public List<CreateOrderItemVM> CreateListofOrders(List<ViewCart_itemVM> viewCart_ItemVMs,CreateOrderVM createOrderVM)
        {
            List<CreateOrderItemVM> createOrderItemVMs = new();
            foreach (var itemVM in viewCart_ItemVMs)
            {
                CreateOrderItemVM createOrderItemVM = new()
                {
                    OrderId = createOrderVM.OrderId,
                    productname = _productService.GetProductByID(itemVM.ProductId).ProductName,
                    quantity = itemVM.quantity,
                    basictotal = (_productService.GetProductByID(itemVM.ProductId).SellPrice) * itemVM.quantity
                };
                createOrderItemVM = CreateOrderitem(createOrderItemVM);
                createOrderItemVMs.Append(createOrderItemVM);
            }
            return createOrderItemVMs;

        }
    }
}
