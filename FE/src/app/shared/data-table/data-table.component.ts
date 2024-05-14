import { Component, Input, ViewChild, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../modal/modal.component';
import { IService } from '../../services/IService';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { GeofenceService } from '../../services/geofence.service';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css'],
})
// I know, the class is too long, but this is for the reusability of the component, and it's impossible for me break it down into smaller components,
// sure it could be better, but I'm not sure how to do it, so I'm leaving it as it is.
export class DataTableComponent implements OnInit {
  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router,
    private vehicleService: VehicleService,
    private geofenceService: GeofenceService
  ) {}

  ngOnInit() {
    this.getData();
  }

  data: any[] = [];
  displayedColumns: string[] = [];
  @Input() fields: string[] = []; // Fields for the modal (dialog)
  @Input() add: boolean = false; // Does the table have an add button
  @Input() service: IService | undefined; // Service to get data from
  @Input() entity: string = ''; // Entity name
  @Input() actions: string[] = []; // Actions to be performed on the data
  @Input() id: number | null = 0; // ID of the entity (currently only used for Route-History)
  dataSource: MatTableDataSource<any> = new MatTableDataSource(); // Data source for the table
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined; // Paginator for the table
  @ViewChild(MatSort) sort: MatSort | undefined; // Sort for the table

  // Gets data from the service
  getData() {
    this.service?.getAll(this.id).subscribe(
      (data: any) => {
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

  // Updates the table with the new data
  updateTable() {
    this.dataSource = new MatTableDataSource(this.data);
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }

  // Used for searching the table
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  // Opens the modal for adding a new row
  addRow() {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        id: this.id, // ID of the entity (currently only used for Route-History)
        purpose: 'ADD', // Purpose of the modal
        history: this.entity === 'Route-History' ? true : false, // Is it a route history modal
        fields: this.fields.length === 0 ? this.displayedColumns : this.fields, // Fields for the modal, if not provided, use the displayed columns
      },
    });

    // After the modal is closed, add the new row to the dataBase
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service?.add(result).subscribe(
          (data: any) => {
            // If the data was added successfully, show a snackbar message and update the table(using STS tag in the response GVAR)
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

  // Performs the clicked action
  performAction = (action: string, element: any) => {
    switch (action) {
      case 'DELETE':
        this.performDelete(element);
        break;
      case 'EDIT':
        this.performEdit(element);
        break;

      case 'DETAILS':
        this.vehicleDetails(element);
        break;

      case 'VEHICLE-INFO':
        this.vehicleInfo(element);
        break;

      case 'ROUTE-HISTORY':
        this.router.navigate(['/vehicles', element.VehicleID, 'route-history']);
        break;
    }
  };

  // Performs the delete action
  performDelete(element: any) {
    this.service?.delete(element).subscribe(
      (data: any) => {
        // If the data was deleted successfully, show a snackbar message and update the table(using STS tag in the response GVAR)
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

  // Performs the edit action (opens the modal for editing)
  performEdit(element: any) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        entity: this.entity,
        purpose: 'Edit',
        fields: this.fields.length === 0 ? this.displayedColumns : this.fields,
        element: element,
      },
    });

    // After the modal is closed, update the row in the dataBase
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service?.update(result).subscribe(
          (data: any) => {
            // If the data was updated successfully, show a snackbar message and update the table(using STS tag in the response GVAR)
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

  // Gets the details of the vehicle
  vehicleDetails(element: any) {
    this.vehicleService?.getDetails(element).subscribe(
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

  // Performs Add, Update, Delete operations on the vehicle info depending on if the vehicle has info or not
  vehicleInfo(element: any) {
    let hasInfo: boolean;
    this.vehicleService?.getDetails(element).subscribe((details: any) => {
      hasInfo =
        details[0].VehicleMake.trim() === '' &&
        details[0].VehicleModel.trim() === '' &&
        details[0].DriverName.trim() === ''
          ? false
          : true;

      this.vehicleService?.getInfo(element).subscribe((data: any) => {
        const dialogRef = this.dialog.open(DialogComponent, {
          data: {
            purpose: 'VEHICLE-INFO',
            fields: ['Driver', 'VehicleMake', 'VehicleModel', 'PurchaseDate'],
            info: data,
            hasInfo: hasInfo,
            element: element,
          },
        });

        // After the modal is closed, perform the action on the vehicle info
        dialogRef.afterClosed().subscribe((result) => {
          if (result) {
            // perform the action on the vehicle info
            switch (result.action) {
              // Delete the vehicle info
              case 'delete':
                this.vehicleService?.deleteVehicleInfo(result.data).subscribe(
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
              // Add the vehicle info
              case 'add':
                this.vehicleService?.addVehicleInfo(result.data).subscribe(
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
              // Update the vehicle info
              case 'update':
                this.vehicleService?.updateVehicleInfo(result.data).subscribe(
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
