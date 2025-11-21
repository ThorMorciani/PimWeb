import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ɵInternalFormsSharedModule, ReactiveFormsModule } from '@angular/forms';
import { MatCommonModule } from '@angular/material/core';
import { CommonModule } from '@angular/common'; 
import { ProfileService } from '../../../../core/services/profile/profile.service';

@Component({
  selector: 'app-modal-user',
  standalone: true,
  imports: [ReactiveFormsModule,MatCommonModule, ɵInternalFormsSharedModule, CommonModule],
  templateUrl: './modal-user.component.html',
  styleUrl: './modal-user.component.scss'
})
export class ModalUserComponent implements OnInit{
  @Input() set userData(value: any) {
    if (value) {
      this.form.patchValue(value);
      this.form.patchValue({profileId: value.profile.id});
      this.isEdit = true;
      this.setPasswordValidator(true);
    } else {
      this.setPasswordValidator(false);
    }
  }
  @Output() formSubmitted = new EventEmitter<{ formData: any, isEdit: boolean}>();

  form: FormGroup;
  showModal = false;
  profiles: any;
  isEdit: boolean = false;

  constructor(private fb: FormBuilder, private profileService: ProfileService) {
    this.form = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password:[''],
      active: [true],
      profileId: ['', Validators.required]
    });
  }

  setPasswordValidator(isEdit: boolean) {
    const passwordControl = this.form.get('password');
    if (isEdit)
      passwordControl?.clearValidators();
    else 
      passwordControl?.setValidators([Validators.required]);

    passwordControl?.updateValueAndValidity();
  }

  ngOnInit(): void {
    this.profileService.getProfiles()
    .subscribe({
      next: (resp) => {
        this.profiles = resp.map(item => ({
          id: item.id,
          profile: item.profileName
        }));
      }
    });
  }

  open() {
    this.showModal = true;
  }

  close() {
    this.form.reset();
    this.showModal = false;
  }

  submit() {
    if (this.form.valid) {
      this.formSubmitted.emit({
        formData: this.form.value,
        isEdit: this.isEdit
      });
      this.close();
    } else
        this.form.markAllAsTouched();
  }
}
