using BackOfficeApp_Domain.Common;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Accounting;

public class AccountingNoteForm : FormTemplate
{
    public AccountingNoteForm() : base("Formularz Nota Księgowa", "Formularz do stworzenia Noty Księgowej", "accountingNoteForm", "NK", "Accounting", "Rejestracja", 4)
    {
        Statuses = new List<string>
            {
                "Rejestracja", "Otwarte", "Zamkniete"
            };
    }
    public string DG { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountRemaining { get; set; }
    public string NotesOrPayments { get; set; }
    public bool Exported { get; set; }
    public string MPK { get; set; }
    public string NoteNumber { get; set; }
    public DateTime Date { get; set; }
    public string ServiceAdvisor { get; set; }
    public string Dealer { get; set; }
    public string Insurer { get; set; }
    public string VIN { get; set; }
    public string Registration { get; set; }
    public string OrderNumber { get; set; }
    public string DamageNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public string PaymentMethod { get; set; }
    public string DealerName { get; set; }
    public string NoteContent { get; set; }
    public string Attachment { get; set; }
}