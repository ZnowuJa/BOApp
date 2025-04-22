using Application.AdHocJobs;
using Application.Forms.Accounting;
using Application.Mappings;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using Domain.Forms.Accounting;

namespace Application.Forms
{
   public class AccountingNoteFormVm : IMapFrom<AccountingNoteForm>
    {
        // Properties from FormTemplate
        public int Id { get; set; }
        public string Name { get; set; } = "Formularz Nota Księgowa";
        public string Description { get; set; } = "Formularz Noty Księgowej";
        public string FolderName { get; set; } = "accountingNoteForm";
        public string NumberPrefix { get; set; } = "NK";
        public string Status { get; set; } = "Rejestracja";
        public List<string> Statuses { get; set; } = ["Rejestracja", "Otwarte", "Zamkniete"];
        public int WorkflowTemplateId { get; set; }

        //Properties specific to SmallAuditableEntity
        public string CreatedBy { get; set; }

        //Properties specific to AccountingNoteForm
        public string DG { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; } = 0;
        public string NotesOrPayments { get; set; } = string.Empty;
        public bool Exported { get; set; } = false;
        public string MPK { get; set; } = string.Empty;
        public string NoteNumber { get; set; } = string.Empty;
        public DateTime? Date { get; set; } 
        public string ServiceAdvisor { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string Dealer { get; set; } = string.Empty;
        public string Insurer { get; set; } = string.Empty;
        public string VIN { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string DamageNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime? PaymentDeadline { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string DealerName { get; set; } = string.Empty;
        public string NoteContent { get; set; } = string.Empty;
        //public string Attachment { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Attachment> Attachments { get; set; } = new();

        //
        public string? CompanyVmName { get; set; }
        public int? CompanyVmId { get; set; }
        public CompanyVm? companiesVm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AccountingNoteForm, AccountingNoteFormVm>()
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.Attachment)
            ? new List<Attachment>()
            : AppUtils.SafeDeserialize<List<Attachment>>(src.Attachment)));

            profile.CreateMap<AccountingNoteFormVm, AccountingNoteForm>()
                .ForMember(dest => dest.Attachment, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Attachments)));
        }
    }

}
