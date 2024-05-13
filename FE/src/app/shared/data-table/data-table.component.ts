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
import { Router } from '@angular/router';

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
    this.service?.getAll(this.id).subscribe(
      (data: any) => {
        console.log(data);
        this.data = data;
        this.updateTable();
        this.displayedColumns = Object.keys(this.data[0]);
        if (this.actions.length > 0) this.displayedColumns.push('Actions');
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

  updateTable() {
    this.dataSource = new MatTableDataSource(this.data);
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }

  data: any[] = [];
  displayedColumns: string[] = [];
  @Input() fields: string[] = [];
  @Input() add: boolean = false;
  @Input() service: IService | undefined;
  @Input() entity: string = '';
  @Input() actions: string[] = [];
  @Input() id: number | null = 0;
  @Input() isHistory: boolean = false;
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

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
        id: this.id,
        purpose: 'ADD',
        history: this.isHistory,
        fields: this.fields.length === 0 ? this.displayedColumns : this.fields,
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

      case 'DETAILS':
        this.details(element);
        break;

      case 'VEHICLE-INFO':
        this.vehicleInfo(element);
        break;

      case 'ROUTE-HISTORY':
        this.router.navigate(['/vehicles', element.VehicleID, 'route-history']);
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
          this.updateTable();
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
        fields: this.fields.length === 0 ? this.displayedColumns : this.fields,
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
              this.updateTable();
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

  details(element: any) {
    this.service?.getDetails(element).subscribe(
      (data: any) => {
        const fields = Object.keys(data[0]);
        const dialogRef = this.dialog.open(DialogComponent, {
          data: {
            purpose: 'DETAILS',
            fields: fields,
            element: data[0],
          },
        });
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

  vehicleInfo(element: any) {
    let hasInfo: boolean;
    this.service?.getDetails(element).subscribe((details: any) => {
      hasInfo =
        details[0].VehicleMake.trim() === '' &&
        details[0].VehicleModel.trim() === '' &&
        details[0].DriverName.trim() === ''
          ? false
          : true;

      this.service?.getInfo(element).subscribe((data: any) => {
        const dialogRef = this.dialog.open(DialogComponent, {
          data: {
            purpose: 'VEHICLE-INFO',
            fields: ['Driver', 'VehicleMake', 'VehicleModel', 'PurchaseDate'],
            info: data,
            hasInfo: hasInfo,
            element: element,
          },
        });

        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            switch (result.action) {
              case 'delete':
                this.service?.deleteVehicleInfo(result.data).subscribe(
                  (data: any) => {
                    this.snackBar.open('Delete Successful', 'Close', {
                      duration: 2000,
                    });
                    this.updateTable();
                  },
                  (error) => {
                    this.snackBar.open('Delete Failed', 'Close', {
                      duration: 2000,
                    });
                  }
                );
                break;
              case 'add':
                this.service?.addVehicleInfo(result.data).subscribe(
                  (data: any) => {
                    this.snackBar.open('Add Successful', 'Close', {
                      duration: 2000,
                    });
                    this.updateTable();
                  },
                  (error) => {
                    this.snackBar.open('Add Failed', 'Close', {
                      duration: 2000,
                    });
                  }
                );
                break;
              case 'update':
                this.service?.updateVehicleInfo(result.data).subscribe(
                  (data: any) => {
                    this.snackBar.open('Update Successful', 'Close', {
                      duration: 2000,
                    });
                    this.updateTable();
                  },
                  (error) => {
                    this.snackBar.open('Update Failed', 'Close', {
                      duration: 2000,
                    });
                  }
                );
                break;
            }
          }
        });
      });
    });
  }
}
