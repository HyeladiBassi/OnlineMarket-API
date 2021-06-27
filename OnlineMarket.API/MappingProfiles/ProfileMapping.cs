using System.Collections.Generic;
using AutoMapper;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.DataTransferObjects.Deliver;
using OnlineMarket.DataTransferObjects.Media;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.ProductReview;
using OnlineMarket.DataTransferObjects.SystemUser;
using OnlineMarket.DataTransferObjects.Transaction;
using OnlineMarket.Models;

namespace OnlineMarket.API.MappingProfiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {

            #region Authentication
            CreateMap<SellerSignUpDto, SystemUser>();
            CreateMap<BuyerSignUpDto, SystemUser>();
            CreateMap<SystemUser, SystemUserViewDto>()
                .ForMember(x => x.Bank, map => map.MapFrom(x => x.BankName));
            CreateMap<ModeratorCreateDto, SystemUser>();
            #endregion

            #region Product
            CreateMap<ProductCreateDto, Product>()
                .ForPath(x => x.Category.Name, map => map.MapFrom(x => x.Category));
            CreateMap<Product, ProductViewDto>()
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Price, map => map.MapFrom(x => x.Price))
                .ForMember(x => x.Description , map => map.MapFrom(x => x.Description))
                .ForMember(x => x.WarehouseLocation, map => map.MapFrom(x => x.WarehouseLocation))
                .ForMember(x => x.Stock, map => map.MapFrom(x => x.Stock))
                .ForMember(x => x.Category, map => map.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Seller, map => map.MapFrom(x => x.Seller))
                .ForMember(x => x.AverageRating, map => map.MapFrom(x => x.AverageRating))
                .ForMember(x => x.Status, map => map.MapFrom(x => x.Status));
            #endregion

            #region Product Review
            CreateMap<ProductReviewCreateDto, ProductReview>();
            CreateMap<ProductReview, ProductReviewViewDto>()
                .ForMember(x => x.ReviewerName, map => map.MapFrom(x => x.Reviewer.FirstName))
                .ForMember(x => x.DateCreated, map => map.MapFrom(x => x.DateCreated));
            #endregion

            #region Images
            CreateMap<Image, MediaViewDto>()
				.ForMember(x => x.Id, map => map.MapFrom(x => x.Id))
				.ForMember(x => x.IsMain, map => map.MapFrom(x => x.IsMain))
				.ForMember(x => x.Link, map => map.MapFrom(x => x.Link))
				.ForMember(x => x.MimeType, map => map.MapFrom(x => x.MimeType))
				.ForMember(x => x.Type, map => map.MapFrom(x => x.Type));
            #endregion

            #region Delivery
            CreateMap<DeliveryCreateDto, Delivery>();
            CreateMap<Delivery, DeliveryViewDto>();
            #endregion

            #region Transaction
            CreateMap<TransactionCreateDto, Transaction>();
            CreateMap<Transaction, TransactionViewDto>();
            #endregion

            #region Wishlist
            CreateMap<WishListItem, WishListViewDto>();
            CreateMap<Product, WishListItemViewDto>()
                .ForMember(x => x.Id, map => map.MapFrom(x => x.Id))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Price, map => map.MapFrom(x => x.Price))
                .ForMember(x => x.WarehouseLocation, map => map.MapFrom(x => x.WarehouseLocation))
                .ForMember(x => x.AverageRating, map => map.MapFrom(x => x.AverageRating));
            #endregion

            #region Order
            CreateMap<Order, OrderViewDto>();
            #endregion
            
        }
    }
}