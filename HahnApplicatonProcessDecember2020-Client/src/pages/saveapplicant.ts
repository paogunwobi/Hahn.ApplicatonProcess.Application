
import {ValidationControllerFactory, ValidationRules} from 'aurelia-validation';
import { ApplicantService } from './../services/applicantservice';
import { bindable, autoinject } from 'aurelia-framework';

@autoinject
export class Saveapplicant {
  @bindable applicant;
  id: number;
  name: ''; 
  familyName: '';
  address: '';
  countryOfOrigin: '';
  emailAddress: '';
  age: number;
  hired = false;
  errors = null;
  controller: any;
  valid = false;
  routeConfig;
  value = false;

  constructor(private applicantService: ApplicantService, private controllerFactory: ValidationControllerFactory) {

      this.controller = this.controllerFactory.createForCurrentScope();
  
      ValidationRules
        .ensure('emailAddress').required().email()
        .ensure('name').required().minLength(5)
        .ensure('familyName').required().minLength(5)
        .ensure('address').required().minLength(10)
        .ensure('countryOfOrigin').required().minLength(3)
        .ensure('age').required().min(20).max(60)
        .on(this);
    
  }

  async activate(params, routeConfig) {
    this.reset();
    this.routeConfig = routeConfig;
    this.id = params.id;

    if (this.id) {
      this.applicant = await this.applicantService.getApplicant(this.id);
      this.id = this.applicant?.id;
      this.name = this.applicant?.name;
      this.familyName = this.applicant?.familyName;
      this.address = this.applicant?.address;
      this.countryOfOrigin = this.applicant?.countryOfOrigin;
      this.emailAddress = this.applicant?.emailAddress;
      this.age = this.applicant?.age;
      this.hired = this.applicant?.hired;
    }
    
  }
    get canreset() {
      return this.name !== '' || this.familyName !== '' || this.address !== '' || this.emailAddress !== '';
    }

    reset() {
      this.id = null;
      this.name = '';
      this.familyName = '';
      this.address = '';
      this.countryOfOrigin = '';
      this.emailAddress = '';
      this.age = null;
      this.hired = false;
    }

    get canSave() {
      this.check();
      return this.value;
    }

    async check() {
      const result2 = await this.controller.validate();
      this.value = result2.valid;
      return this.value;
    }

    async saveApplicant() {
      this.errors = null;
      
      const result = await this.controller.validate();
      
      if (result.valid) {
        this.valid = true
        const applicant = {
          id: undefined,
          name: this.name,  
          familyName: this.familyName,
          address: this.address,
          countryOfOrigin: this.countryOfOrigin,
          emailAddress: this.emailAddress,
          age: this.age,
          hired: this.hired
        };

        if ( this.id !== undefined ) {
          applicant.id = this.id;
        }

        this.applicant.id ? await this.applicantService.putApplicant(this.applicant.id, applicant) 
                : await this.applicantService.postApplicant(applicant);   
      
      }
  }
}
