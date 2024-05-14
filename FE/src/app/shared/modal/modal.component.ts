import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-dialog',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
})
export class DialogComponent {
  formGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    const controls: { [key: string]: any } = {};

    // Create form controls for each field
    this.fields.forEach((field: string) => {
      // If the purpose is to edit, set the value of the form control to the current value of the field
      if (this.data['purpose'] === 'Edit') {
        controls[field] = [this.data['element'][field], Validators.required];
      } else if (field === 'RecordTime' || field === 'PurchaseDate') {
        // If the field is RecordTime, set the value to the current date and time
        let date = new Date();
        // Format the date to 'MM/DD/YYYY HH:MM:SS'
        let formattedDate =
          date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
          }) +
          ' ' +
          date.toLocaleTimeString();
        controls[field] = [formattedDate, Validators.required];
      } else {
        // Otherwise, set the value to an empty string (this is the default field)
        controls[field] = ['', Validators.required];
      }
    });

    this.formGroup = this.formBuilder.group(controls);

    // If the purpose is to view details, set the form to the current element and disable it (as it is read-only mode)
    if (this.data['purpose'] === 'DETAILS') {
      this.formGroup.patchValue(this.data['element']);
      this.formGroup.disable();
    }
  }

  // Remove the ID fields and the Actions field from the fields array
  fields: string[] = this.data['fields'].reduce(
    (acc: string[], field: string) => {
      if (
        (acc.length === 0 && field.toLowerCase().endsWith('id')) ||
        field === 'Actions'
      ) {
        return acc;
      }
      return [...acc, field];
    },
    []
  );

  // Close the dialog
  onCancel(): void {
    this.dialogRef.close();
  }

  // Confirm the action
  onConfirm(): void {
    const entity = this.formGroup.value;
    // If the purpose is to edit, set the ID field to the current ID(since we removed it from the form)
    if (this.data['purpose'] === 'Edit') {
      const id = this.data['fields']?.filter((field: string) =>
        field.toLowerCase().endsWith('id')
      )[0];
      entity[id] = this.data['element'][id].toString();
    }
    // If the modal is for route history, set the VehicleID to the current ID of the vehicle
    if (this.data['history']) {
      entity['VehicleID'] = this.data['id'];
    }
    this.dialogRef.close(entity);
  }

  // These last methods are for the vehicle Info modal exclusively
  // Add the vehicle Info
  onAdd(): void {
    const result = {
      VehicleID: this.data.element.VehicleID,
      DriverID: this.formGroup.value.Driver.toString(),
      VehicleMake: this.formGroup.value.VehicleMake,
      VehicleModel: this.formGroup.value.VehicleModel,
      PurchaseDate: this.formGroup.value.PurchaseDate,
    };

    this.dialogRef.close({ action: 'add', data: result });
  }

  // Delete the vehicle Info
  onDelete(): void {
    this.dialogRef.close({ action: 'delete', data: this.data['element'] });
  }

  // Update the vehicle Info
  onUpdate(): void {
    const result = {
      VehicleID: this.data.element.VehicleID,
      DriverID: this.formGroup.value.Driver.toString(),
      VehicleMake: this.formGroup.value.VehicleMake,
      VehicleModel: this.formGroup.value.VehicleModel,
      PurchaseDate: this.formGroup.value.PurchaseDate,
    };
    this.dialogRef.close({ action: 'update', data: result });
  }
}
