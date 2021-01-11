import { HttpClient, json } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';
@autoinject
export class ApplicantService {

  
    constructor(private http: HttpClient) {
        http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl('https://localhost:5001/api/applicant');
        });
    }

    async getApplicant(Id) {
      try {
        let response = await this.http.fetch(`/${Id}`);
        let data = await response.json();
        return data;
      } catch(error) {
          console.log(error);
          return null;
      }
    }

    async getApplicants() {
      try {
        let response = await this.http.fetch('');
        let data = await response.json();
        return data;
      } catch(error) {
          console.log(error);
          return null;
      }
    }

    async postApplicant(applicant) {
      console.log('Posting: ', applicant);
      try {
        let response = await this.http.fetch('', {method: 'post', body: json(applicant)});
        let data = await response.json();
        return data;
      } catch (error) {
          console.log(error);
          return null;
      }
    }

    async putApplicant(id, applicant) {
      console.log('Updating: ', applicant, 'with ID: ', id);
      try {
        let response = await this.http.fetch(`/${id}`, {method: 'put', body: json(applicant)});
        let data = await response.json();
        return data;
      } catch (error) {
          console.log(error);
          return null;
      }
    }

    async deleteApplicant(id) {
      console.log('Deleting Applicant with ID: ', id);
      try {
        let response = await this.http.fetch(`/${id}`, {method: 'delete'});
        let data = await response.json();
        return data;
      } catch (error) {
          console.log(error);
          return null;
      }
    }
}
