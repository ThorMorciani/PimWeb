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
export class ModalUserComponent implements OnChanges, OnInit{
  @Input() formData: any | null = null
  @Output() formSubmitted = new EventEmitter<{ formData: any, confirmed: boolean }>();

  form: FormGroup;
  showModal = false;
  profiles: any;

  constructor(private fb: FormBuilder, private profileService: ProfileService) {
    this.form = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', Validators.required],
      active: [true],
      profileId: ['', Validators.required]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['formData'] && this.form) {
      this.form.patchValue(this.formData);
    }
  }

  ngOnInit(): void {
    this.profileService.getProfiles()
    .subscribe({
      next: (resp) => {
        console.log(resp);
        this.profiles = resp.map(item => ({
          id: item.id,
          profile: item.profileName
        }));
      }
    });
    console.log(this.profiles);
  }

  open() {
    this.showModal = true;
  }

  close() {
    this.showModal = false;
  }

  submit() {
    if (this.form.valid) {
      this.formSubmitted.emit({
        formData: this.form.value,
        confirmed: true
      });
      this.close();
    } else {
      this.form.markAllAsTouched();
    }
  }
}
