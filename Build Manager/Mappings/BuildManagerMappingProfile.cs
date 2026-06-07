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
            CreateMap<Company, CompanyResponseDto>();

            // ── CompanyUser ───────────────────────────────────────────────────
            CreateMap<CompanyUserRequestDto, CompanyUser>()
                .ForMember(d => d.PasswordHash, opt => opt.Ignore());
            CreateMap<CompanyUser, CompanyUserResponseDto>()
                .ForMember(d => d.CompanyName,
                           opt => opt.MapFrom(s => s.Company != null ? s.Company.CompanyName : string.Empty));

            // ── CompanyBank ───────────────────────────────────────────────────
            CreateMap<CompanyBankRequestDto, CompanyBank>();
            CreateMap<CompanyBank, CompanyBankResponseDto>();      // AccountNumber intentionally absent from response DTO

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
            CreateMap<Client, ClientResponseDto>();               // Response omits DoorNo, FaxNumber, AccountNumber, TaxIDs

            // ── Supplier ──────────────────────────────────────────────────────
            CreateMap<SupplierRequestDto, Supplier>();
            CreateMap<Supplier, SupplierResponseDto>();           // Response omits DoorNo, FaxNumber, AccountNumber, TaxIDs

            // ── SubContractor ─────────────────────────────────────────────────
            CreateMap<SubContractorRequestDto, SubContractor>();
            CreateMap<SubContractor, SubContractorResponseDto>()  // Response omits DoorNo, FaxNumber, AccountNumber, TaxIDs, ESR
                .ForMember(d => d.Rate,
                           opt => opt.MapFrom(s => s.Rate));

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
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());   // computed property

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
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());   // computed property

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
                .ForMember(d => d.BalanceAmount, opt => opt.Ignore());   // computed property
        }
    }
}
