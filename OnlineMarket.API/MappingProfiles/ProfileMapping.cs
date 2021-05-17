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
            CreateMap<BuyerSignUpDto, SystemUser>();
            CreateMap<SystemUser, SystemUserViewDto>();
            #endregion

            #region Product
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductViewDto>()
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.PaymentMethod, map => map.MapFrom(x => x.PaymentMethod))
                .ForMember(x => x.Price, map => map.MapFrom(x => x.Price))
                .ForMember(x => x.Description , map => map.MapFrom(x => x.Description))
                .ForMember(x => x.Stock, map => map.MapFrom(x => x.Stock))
                .ForMember(x => x.Category, map => map.MapFrom(x => x.Category))
                .ForMember(x => x.Seller, map => map.MapFrom(x => x.Seller));
            #endregion

            #region Product Review
            CreateMap<ProductReviewCreateDto, ProductReview>();
            CreateMap<ProductReview, ProductReviewViewDto>()
                .ForMember(x => x.ReviewerName, map => map.MapFrom(x => x.Reviewer.FirstName +" "+ x.Reviewer.LastName));
            #endregion

            #region Images
            CreateMap<Image, MediaViewDto>()
				.ForMember(x => x.Link, map => map.MapFrom(x => x.Link))
				.ForMember(x => x.MimeType, map => map.MapFrom(x => x.MimeType))
				.ForMember(x => x.Type, map => map.MapFrom(x => x.Type));
            #endregion

            #region Delivery
            CreateMap<DeliveryCreateDto, Delivery>();
            #endregion

            #region Transaction
            CreateMap<TransactionCreateDto, Transaction>();
            #endregion
            
        }
    }
}