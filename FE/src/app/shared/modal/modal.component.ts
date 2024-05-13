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

    this.fields.forEach((field: string) => {
      if (this.data['purpose'] === 'Edit') {
        controls[field] = [this.data['element'][field], Validators.required];
      } else if (field === 'Status') {
        controls[field] = [
          '',
          [Validators.required, Validators.pattern('^[01]$')],
        ];
      } else if (field === 'RecordTime') {
        let date = new Date();
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
        controls[field] = ['', Validators.required];
      }
    });

    this.formGroup = this.formBuilder.group(controls);

    if (this.data['purpose'] === 'DETAILS') {
      this.formGroup.patchValue(this.data['element']);
      this.formGroup.disable();
    }
  }

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

  entity: any;

  onCancel(): void {
    this.dialogRef.close();
  }

  onConfirm(): void {
    const entity = this.formGroup.value;
    if (this.data['purpose'] === 'Edit') {
      const id = this.data['fields']?.filter((field: string) =>
        field.toLowerCase().endsWith('id')
      )[0];
      entity[id] = this.data['element'][id].toString();
    }
    if (this.data['history']) {
      entity['VehicleID'] = this.data['id'];
    }
    this.dialogRef.close(entity);
  }

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

  onDelete(): void {
    this.dialogRef.close({ action: 'delete', data: this.data['element'] });
  }

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
