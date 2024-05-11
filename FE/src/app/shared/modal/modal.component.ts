import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-dialog',
  templateUrl: './modal.component.html',
})
export class DialogComponent {
  formGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    const controls: { [key: string]: any } = {};
    this.fields?.forEach((field: string) => {
      if (this.data['purpose'] === 'Edit') {
        controls[field] = [
          this.data['element'][field].toString(),
          Validators.required,
        ];
        return;
      }
      controls[field] = ['', Validators.required];
    });
    this.formGroup = this.formBuilder.group(controls);
  }
  fields: string[] = this.data['fields'].filter(
    (field: string) =>
      field !== 'Actions' && !field.toLowerCase().endsWith('id')
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
    this.dialogRef.close(entity);
  }
}
