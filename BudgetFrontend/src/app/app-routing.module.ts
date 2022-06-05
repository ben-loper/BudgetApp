import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonthlyIncomeComponent } from './monthly-income/monthly-income.component';
import { BudgetComponent } from './budget/budget.component';
import { MonthlyBillComponent } from './monthly-bill/monthly-bill.component';

const routes: Routes = [
  { path: '', redirectTo: 'incomes', pathMatch: 'full' },
  { path: 'incomes', component: MonthlyIncomeComponent },
  { path: 'budgets/:id', component: BudgetComponent },
  { path: 'monthlyBills/:monthlyIncomeId', component: MonthlyBillComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
