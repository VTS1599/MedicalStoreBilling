//using MedBilling.Models;
using CoreBusiness;
using Microsoft.AspNetCore.Mvc;
using UseCases;
//using UseCases;

namespace MedBilling.ViewComponents
{
	[ViewComponent]
	public class TransactionsViewComponent : ViewComponent
	{
		private readonly IGetTodayTransactionsUseCase getTodayTransactionsUseCase;

		public TransactionsViewComponent(IGetTodayTransactionsUseCase getTodayTransactionsUseCase)
		{
			this.getTodayTransactionsUseCase = getTodayTransactionsUseCase;
		}

		public IViewComponentResult Invoke(string userName)
		{
			var transactions = getTodayTransactionsUseCase.Execute(userName);

			//var transactions = TransactionRepository.GetByDayAndCashier(userName,DateTime.Now);
			return View(transactions);
		}
	}
}
