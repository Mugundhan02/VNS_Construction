using AutoMapper;
using BuildManager.DTOs;
using BuildManager.Models;

namespace BuildManager.Mappings
{
    public class BuildManagerMappingProfile : Profile
    {
        public BuildManagerMappingProfile()
        {
            // ── Company ───────────────────────────────────────────────────────
            CreateMap<CompanyRequestDto, Company>();
            CreateMap<Company, CompanyResponseDto>()
                .ForMember(d => d.PinCode, opt => opt.MapFrom(s => s.Address != null ? s.Address.PinCode : string.Empty))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.PhoneNumber : string.Empty))
                .ForMember(d => d.MobileNumber, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.MobileNumber : string.Empty))
                .ForMember(d => d.EmailId, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.EmailId : string.Empty))
                .ForMember(d => d.PanCardNumber, opt => opt.MapFrom(s => s.IdentityDetails != null ? s.IdentityDetails.PanCardNumber : string.Empty))
                .ForMember(d => d.TinNumber, opt => opt.MapFrom(s => s.IdentityDetails != null ? s.IdentityDetails.TinNumber : string.Empty))
                .ForMember(d => d.CstNumber, opt => opt.MapFrom(s => s.IdentityDetails != null ? s.IdentityDetails.CstNumber : string.Empty));

            // ── CompanyUser ───────────────────────────────────────────────────
            CreateMap<CompanyUserRequestDto, CompanyUser>()
                .ForMember(d => d.PasswordHash, opt => opt.Ignore());
            CreateMap<CompanyUser, CompanyUserResponseDto>()
                .ForMember(d => d.CompanyName,
                           opt => opt.MapFrom(s => s.Company != null ? s.Company.CompanyName : string.Empty));

            // ── CompanyBank ───────────────────────────────────────────────────
            CreateMap<CompanyBankRequestDto, CompanyBank>();
            CreateMap<CompanyBank, CompanyBankResponseDto>();

            // ── OfficeExpense ─────────────────────────────────────────────────
            CreateMap<OfficeExpenseRequestDto, OfficeExpense>();
            CreateMap<OfficeExpense, OfficeExpenseResponseDto>();

            // ── PaymentType ───────────────────────────────────────────────────
            CreateMap<PaymentTypeRequestDto, PaymentType>();
            CreateMap<PaymentType, PaymentTypeResponseDto>();

            // ── Whom ──────────────────────────────────────────────────────────
            CreateMap<WhomRequestDto, Whom>();
            CreateMap<Whom, WhomResponseDto>();

            // ── InstallmentTerm ───────────────────────────────────────────────
            CreateMap<InstallmentTermRequestDto, InstallmentTerm>();
            CreateMap<InstallmentTerm, InstallmentTermResponseDto>();

            // ── Client ────────────────────────────────────────────────────────
            CreateMap<ClientRequestDto, Client>();
            CreateMap<Client, ClientResponseDto>()
                .ForMember(d => d.PinCode, opt => opt.MapFrom(s => s.Address != null ? s.Address.PinCode : string.Empty))
                .ForMember(d => d.MobileNumber, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.MobileNumber : string.Empty))
                .ForMember(d => d.EmailId, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.EmailId : string.Empty))
                // Fixed to cleanly target Option A properties (.Unit, .Rate, .Amount)
                .ForMember(d => d.EstimateUnit, opt => opt.MapFrom(s => s.EstimateDetails != null ? s.EstimateDetails.Unit : 0))
                .ForMember(d => d.EstimateRate, opt => opt.MapFrom(s => s.EstimateDetails != null ? s.EstimateDetails.Rate : 0))
                .ForMember(d => d.EstimateAmount, opt => opt.MapFrom(s => s.EstimateDetails != null ? s.EstimateDetails.Amount : 0));

            // ── Supplier ──────────────────────────────────────────────────────
            CreateMap<SupplierRequestDto, Supplier>();
            CreateMap<Supplier, SupplierResponseDto>()
                .ForMember(d => d.PinCode, opt => opt.MapFrom(s => s.Address != null ? s.Address.PinCode : string.Empty))
                .ForMember(d => d.MobileNumber, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.MobileNumber : string.Empty))
                .ForMember(d => d.EmailId, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.EmailId : string.Empty));

            // ── SubContractor ─────────────────────────────────────────────────
            CreateMap<SubContractorRequestDto, SubContractor>();
            CreateMap<SubContractor, SubContractorResponseDto>()
                .ForMember(d => d.PinCode, opt => opt.MapFrom(s => s.Address != null ? s.Address.PinCode : string.Empty))
                .ForMember(d => d.MobileNumber, opt => opt.MapFrom(s => s.ContactInfo != null ? s.ContactInfo.MobileNumber : string.Empty))
                .ForMember(d => d.Rate, opt => opt.MapFrom(s => s.WorkDetails != null ? s.WorkDetails.Rate : 0));

            // ── Material ──────────────────────────────────────────────────────
            CreateMap<MaterialRequestDto, Material>();
            CreateMap<Material, MaterialResponseDto>();

            // ── JobWork ───────────────────────────────────────────────────────
            CreateMap<JobWorkRequestDto, JobWork>();
            CreateMap<JobWork, JobWorkResponseDto>();

            // ── ClientTransaction ─────────────────────────────────────────────
            CreateMap<ClientTransactionRequestDto, ClientTransaction>();
            CreateMap<ClientTransaction, ClientTransactionResponseDto>()
                .ForMember(d => d.ClientName,
                           opt => opt.MapFrom(s => s.Client != null ? s.Client.ClientName : string.Empty))
                .ForMember(d => d.PaymentTypeName,
                           opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.PaymentTypeName : null))
                .ForMember(d => d.ByWhomName,
                           opt => opt.MapFrom(s => s.ByWhom != null ? s.ByWhom.WhomName : null));

            // ── SupplierTransaction ───────────────────────────────────────────
            CreateMap<SupplierTransactionRequestDto, SupplierTransaction>();
            CreateMap<SupplierTransaction, SupplierTransactionResponseDto>()
                .ForMember(d => d.ClientName,
                           opt => opt.MapFrom(s => s.Client != null ? s.Client.ClientName : string.Empty))
                .ForMember(d => d.SupplierName,
                           opt => opt.MapFrom(s => s.Supplier != null ? s.Supplier.SupplierName : string.Empty))
                .ForMember(d => d.MaterialName,
                           opt => opt.MapFrom(s => s.Material != null ? s.Material.MaterialName : string.Empty))
                .ForMember(d => d.PaymentTypeName,
                           opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.PaymentTypeName : null))
                .ForMember(d => d.ToWhomName,
                           opt => opt.MapFrom(s => s.ToWhom != null ? s.ToWhom.WhomName : null))
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());

            // ── SubContractorTransaction ──────────────────────────────────────
            CreateMap<SubContractorTransactionRequestDto, SubContractorTransaction>();
            CreateMap<SubContractorTransaction, SubContractorTransactionResponseDto>()
                .ForMember(d => d.ClientName,
                           opt => opt.MapFrom(s => s.Client != null ? s.Client.ClientName : string.Empty))
                .ForMember(d => d.SubContractorName,
                           opt => opt.MapFrom(s => s.SubContractor != null ? s.SubContractor.SubContractorName : string.Empty))
                .ForMember(d => d.JobWorkName,
                           opt => opt.MapFrom(s => s.JobWork != null ? s.JobWork.JobWorkName : string.Empty))
                .ForMember(d => d.PaymentTypeName,
                           opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.PaymentTypeName : null))
                .ForMember(d => d.ToWhomName,
                           opt => opt.MapFrom(s => s.ToWhom != null ? s.ToWhom.WhomName : null))
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());

            // ── CompanyExpenseTransaction ─────────────────────────────────────
            CreateMap<CompanyExpenseTransactionRequestDto, CompanyExpenseTransaction>();
            CreateMap<CompanyExpenseTransaction, CompanyExpenseTransactionResponseDto>()
                .ForMember(d => d.CompanyName,
                           opt => opt.MapFrom(s => s.Company != null ? s.Company.CompanyName : string.Empty))
                .ForMember(d => d.ClientName,
                           opt => opt.MapFrom(s => s.Client != null ? s.Client.ClientName : null))
                .ForMember(d => d.ExpenseName,
                           opt => opt.MapFrom(s => s.OfficeExpense != null ? s.OfficeExpense.ExpenseName : string.Empty))
                .ForMember(d => d.PaymentTypeName,
                           opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.PaymentTypeName : null))
                .ForMember(d => d.ToWhomName,
                           opt => opt.MapFrom(s => s.ToWhom != null ? s.ToWhom.WhomName : null))
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());
        }
    }
}