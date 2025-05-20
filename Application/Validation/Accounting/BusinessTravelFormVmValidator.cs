using Application.Forms.Accounting;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;

using FluentValidation;

namespace Application.Validation.Accounting;
public class BusinessTravelFormVmValidator : AbstractValidator<BusinessTravelFormVm>
{
    public BusinessTravelFormVmValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Wprowadź datę początkową!");
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Wprowadź datę końcową!");
        RuleFor(x => x.EndDate)
            .Must((model, endDate) => model.StartDate < endDate)
            .WithMessage("Data początkowa powinna być wcześniejsza niż data końcowa!")
            .When(x => x.EndDate.HasValue && x.StartDate.HasValue);
        //RuleFor(x => x.EndDate).NotEmpty().WithMessage("Wprowadź datę końcową!");

        //RuleFor(x => x).Must(x => x.StartDate < x.EndDate).WithMessage("Data początkowa powinna być wcześniejsza niż data końcowa!");
        RuleFor(x => x.Destination).NotEmpty().WithMessage("Wybierz lub wprowadź miasto docelowe!");
        RuleFor(x => x.DestinationCountry).ChildRules(country =>
        {
            country.RuleFor(c => c.Name).NotEmpty().WithMessage("Wybierz kraj docelowy!");
        });
        RuleFor(x => x.Objective).NotEmpty().WithMessage("Wybierz cel wyjazdu!");
        RuleFor(x => x.Transportation).NotEmpty().WithMessage("Wybierz środek transportu!");
        When(form => form.Status == "ZaliczkaKasa", () =>
        {
            RuleFor(x => x.CashPayoutNumber).NotEmpty().WithMessage("Wprowadź numer dokumentu kasowego z Austostacji!");
        });
        When(form => form.Status == "KasaRozliczenie", () =>
        {
            RuleFor(x => x.CashReceiptNumber).NotEmpty().WithMessage("Wprowadź numer dokumentu kasowego z Austostacji!");
        });
        When(form => form.Status == "Rejestracja" || form.Status == "Rozliczenie", () =>
        {
            //RuleFor(x => x.FormCostLocation.SapNumber).NotEmpty().WithMessage("Proszę uzupełnić miejsce kosztowe!");
            //RuleFor(x => x.FormCostCenter.MPK).NotEmpty().WithMessage("Proszę uzupełnić lokalizację!");
            RuleFor(x => x.FormCostCenters)
                .Custom((costCenters, context) =>
                {
                    if (costCenters == null || !costCenters.Any())
                    {
                        context.AddFailure("FormCostCenters", "Lista centrów kosztów nie może być pusta.");
                        return;
                    }

                    int totalShare = costCenters.Sum(cc => cc.Share ?? 0);
                    if (totalShare != 100)
                    {
                        context.AddFailure("FormCostCenters", "Suma udziałów musi wynosić 100%.");
                    }

                    foreach (var cc in costCenters)
                    {
                        if (string.IsNullOrEmpty(cc.LocationName) ||
                            string.IsNullOrEmpty(cc.DepartmentName) ||  
                            string.IsNullOrEmpty(cc.ResponsibleManagerName))
                        {
                            context.AddFailure("FormCostCenters", "Każdy wpis musi zawierać lokalizację, dział i osobę odpowiedzialną.");
                        }
                    }

                });
        });
        When(form => form.AdvancePayment, () => 
        {
            RuleFor(x => x.AdvancePaymentAmount).GreaterThan(0).WithMessage("Wprowadź kwotę zaliczki!");

        });
        When(form => form.AdvancePayment && form.AdvancePaymentCash == true, () =>
        {
            RuleFor(x => x.BankAccountNumber)
            .Custom((bankAccountNumber, context) =>
            {
                if (string.IsNullOrEmpty(bankAccountNumber) || !System.Text.RegularExpressions.Regex.IsMatch(bankAccountNumber, "^PL\\d{26}$"))
                {
                    context.AddFailure("Wprowadź poprawny numer konta!");
                }
            });
            //RuleFor(x => x.BankAccountNumber).NotEmpty().WithMessage("Wprowadź numer konta!").Matches("^PL\\d{26}$").WithMessage("Wprowadź poprawny numer konta!");

        });
        When(form => form.AdvancePayment && form.AdvancePaymentCash == false, () =>
        {
            RuleFor(x => x.CashPoint).ChildRules(cashpoint =>
            {
                cashpoint.RuleFor(c => c.SapNumber).NotEmpty().WithMessage("Wybierz kasę!");
            });

        });
        When(form => form.Status == "Rozliczenie" && form.ReceiptPaymentCash == false, () =>
        {
            RuleFor(x => x.CashPointReceipt).ChildRules(cashpoint =>
            {
                cashpoint.RuleFor(c => c.SapNumber).NotEmpty().WithMessage("Wybierz kasę!");
            });

        });
        When(form => form.Status == "Rozliczenie" && form.ReceiptPaymentCash == true, () =>
        {
            RuleFor(x => x.ReceiptBankAccountNumber)
            .Custom((bankAccountNumber, context) =>
            {
                if (string.IsNullOrEmpty(bankAccountNumber) || !System.Text.RegularExpressions.Regex.IsMatch(bankAccountNumber, "^PL\\d{26}$"))
                {
                    context.AddFailure("Wprowadź poprawny numer konta Receiptt!");
                }
            });
            //RuleFor(x => x.BankAccountNumber).NotEmpty().WithMessage("Wprowadź numer konta!").Matches("^PL\\d{26}$").WithMessage("Wprowadź poprawny numer konta!");

        });
        When(form => form.Transportation == "Samochód prywatny", () =>
        {
            RuleFor(x => x.MileageRegister).ChildRules(register =>
            {
                register.RuleFor(r => r.PrivateCarRegistration).NotEmpty().WithMessage("Wprowadź numer rejestracyjny!").Matches("^[A-Za-z]{1,3}\\s?[A-Za-z0-9]{4,5}$").WithMessage("Wprowadź poprawny numer rejestracyjny!");
            });
            RuleFor(x => x.MileageRegister).ChildRules(register =>
            {
                register.RuleFor(r => r.PrivateCarEngineSize).Must(size => size == "do 900 cm3" || size == "powyżej 900 cm3").WithMessage("Proszę wybrać pojemność silnika!");
            });
        });
        When(form => form.Transportation == "Samochód służbowy", () =>
        {
            RuleFor(x => x.CompanyVehicleNumber).NotEmpty().WithMessage("Wyszukaj samochód z listy!");
        });
        When(form => form.Status == "Rozliczenie", () =>
        {
            //RuleFor(s => s.FormCostCenter.MPK).NotEmpty().WithMessage("Wybierz MPK w sekcji Rozliczenie!");
            RuleForEach(s => s.Stages).SetValidator(new StageValidator());
            RuleFor(s => s.FormCostCenters.Count()).GreaterThan(0).WithMessage("Wybierz MPK!");
        });
        When(form => form.Status == "Rozliczenie" && form.Transportation == "Samochód prywatny", () =>
        {
            RuleFor(x => x.MileageRegister).SetValidator(new MileageRegisterValidator());
        });
        When(form => form.Status == "Rozliczenie" && form.Bills.Count > 0, () =>
        {
            RuleForEach(x => x.Bills).SetValidator(new BillValidator());
            //RuleForEach(x => x.Bills).SetValidator(new BillValidator(x.StartDate, x.EndDate));
        });

        When(form => form.Status == "Rozliczenie" && form.Stages.Count() > 1, () =>
        {
            RuleFor(x => x.Stages)
            .Must(BeNonOverlapping)
            .WithMessage("Etapy nie mogą na siebie nachodzić!");
        });

        
        RuleSet("MainDates", () =>
        {
           
        });

    }

    private bool BeNonOverlapping(List<Stage> stages)
    {
        if (stages == null || stages.Count == 0)
            return true;

        // Sort stages by StartDate to ensure we check consecutive periods
        var sortedStages = stages
            .Where(s => s.StartDate.HasValue && s.EndDate.HasValue)
            .OrderBy(s => s.StartDate)
            .ToList();

        for (int i = 0; i < sortedStages.Count - 1; i++)
        {
            var current = sortedStages[i];
            var next = sortedStages[i + 1];

            // Check if current stage overlaps with the next stage
            if (current.EndDate > next.StartDate)
            {
                return false; // Overlap detected
            }
        }

        return true; // No overlap found
    }
}
