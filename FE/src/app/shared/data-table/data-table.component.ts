import {
  Component,
  Input,
  ViewChild,
  OnChanges,
  SimpleChanges,
  OnInit,
  input,
} from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../modal/modal.component';
import { IService } from '../../services/IService';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css'],
})
export class DataTableComponent implements OnChanges, OnInit {
  ngOnInit() {
    this.getData();
  }

  getData() {
    this.service?.getAll().subscribe(
      (data: any) => {
        this.data = data;
        this.dataSource = new MatTableDataSource(this.data);
        this.displayedColumns = Object.keys(this.data[0]);
        if (this.actions.length > 0) this.displayedColumns.push('Actions');
        if (this.paginator) {
          this.dataSource.paginator = this.paginator;
        }
        if (this.sort) {
          this.dataSource.sort = this.sort;
        }
      },
      (error) => {
        // Error callback
        this.snackBar.open('Server is Down!', 'Close', {
          duration: 2000,
        });
      }
    );
  }

  data: any[] = [];
  displayedColumns: string[] = [];
  @Input() add: boolean = false;
  @Input() service: IService | undefined;
  @Input() entity: string = '';
  @Input() actions: string[] = [];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(private dialog: MatDialog, private snackBar: MatSnackBar) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.dataSource.data = this.data;
    }
    if (changes['displayedColumns'] || changes['actions']) {
      this.displayedColumns = [...this.displayedColumns, 'Actions'];
    }
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  addRow() {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        entity: this.entity,
        purpose: 'Add',
        fields: this.displayedColumns,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service?.add(result).subscribe(
          (data: any) => {
            if (data['DicOfDic']['Tags']['STS'] === '1') {
              this.snackBar.open('Added Successfully', 'Close', {
                duration: 2000,
              });
              this.getData();
            } else {
              this.snackBar.open('Add Failed', 'Close', {
                duration: 2000,
              });
            }
          },
          (error) => {
            let errorMessage = 'An unknown error occurred!';
            if (error.status === 404) {
              errorMessage = 'Resource not found!';
            } else if (error.status === 400) {
              errorMessage = 'Bad request!';
            } else if (error.status === 500) {
              errorMessage = 'Server error!';
            }
            this.snackBar.open(errorMessage, 'Close', {
              duration: 2000,
            });
          }
        );
      }
    });
  }

  performAction = (action: string, element: any) => {
    switch (action) {
      case 'DELETE':
        this.performDelete(element);
        break;
      case 'EDIT':
        this.performEdit(element);
        break;

      case 'VIEW':
        break;
    }
  };

  performDelete(element: any) {
    this.service?.delete(element).subscribe(
      (data: any) => {
        if (data['DicOfDic']['Tags']['STS'] === '1') {
          this.snackBar.open('Deleted Successfully', 'Close', {
            duration: 2000,
          });
          this.data = this.data.filter((item) => item !== element);
          this.dataSource = new MatTableDataSource(this.data);
        } else {
          this.snackBar.open('Delete Failed', 'Close', {
            duration: 2000,
          });
        }
      },
      (error) => {
        let errorMessage = 'An unknown error occurred!';
        if (error.status === 404) {
          errorMessage = 'Resource not found!';
        } else if (error.status === 400) {
          errorMessage = 'Bad request!';
        } else if (error.status === 500) {
          errorMessage = 'Server error!';
        }
        this.snackBar.open(errorMessage, 'Close', {
          duration: 2000,
        });
      }
    );
  }

  performEdit(element: any) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        entity: this.entity,
        purpose: 'Edit',
        fields: this.displayedColumns,
        element: element,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service?.update(result).subscribe(
          (data: any) => {
            if (data['DicOfDic']['Tags']['STS'] === '1') {
              this.snackBar.open('Updated Successfully', 'Close', {
                duration: 2000,
              });
              const index = this.data.findIndex((item) => item === element);
              this.data[index] = result;
              this.dataSource = new MatTableDataSource(this.data);
            } else {
              this.snackBar.open('Update Failed', 'Close', {
                duration: 2000,
              });
            }
          },
          (error) => {
            let errorMessage = 'An unknown error occurred!';
            if (error.status === 404) {
              errorMessage = 'Resource not found!';
            } else if (error.status === 400) {
              errorMessage = 'Bad request!';
            } else if (error.status === 500) {
              errorMessage = 'Server error!';
            }
            this.snackBar.open(errorMessage, 'Close', {
              duration: 2000,
            });
          }
        );
      }
    });
  }
}
