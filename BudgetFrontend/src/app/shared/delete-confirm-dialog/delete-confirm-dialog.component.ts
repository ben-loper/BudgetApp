import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from 'src/services/MonthlyIncomeService';

export interface IDeleteDialogData {
  id: number;
  endpoint: string;
  itemName: string;
}

@Component({
  selector: 'app-delete-confirm-dialog',
  templateUrl: './delete-confirm-dialog.component.html',
  styleUrls: ['./delete-confirm-dialog.component.css'],
})
export class DeleteConfirmDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<DeleteConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IDeleteDialogData,
    private service: ApiService
  ) {}

  public id: number;
  public endpoint: string;
  public itemName: string;

  ngOnInit(): void {
    this.id = this.data.id;
    this.endpoint = this.data.endpoint;
    this.itemName = this.data.endpoint;
  }

  submit(): void {
    this.service
      .deleteItem({
        id: this.id,
        endpoint: this.endpoint,
        itemName: this.itemName,
      })
      .subscribe((response) => {
        this.dialogRef.close(response);
      });
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
