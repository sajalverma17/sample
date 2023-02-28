import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-counter-component',
  styleUrls: ['counter.component.css'],
  templateUrl: './counter.component.html'
})

export class CounterComponent {
  public formBuilder;
  public addProductForm;
  public http: HttpClient;
  public baseUrl: string;
  public containers: Container[] = [];

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    formBuilder: FormBuilder) {

    this.http = http;
    this.baseUrl = baseUrl;
    this.formBuilder = formBuilder;

    // Pre-populate containers in DB:
    this.http.get<Container[]>(this.baseUrl + 'container').subscribe(result => {
      this.containers = result;
    }, error => console.log(error));

    this.addProductForm = this.formBuilder.group({
      productName: '',
      containerCode: this.containers,
    });
  }

  public onSubmit(): void {
    let inputProductName = this.addProductForm.controls['productName'].value;
    let inputContainerCode = this.addProductForm.controls['containerCode'].value;

    let data = {
      "name": inputProductName,
      "containerCode": inputContainerCode,
    };

    this.http.post(this.baseUrl + 'product/Insert', data).subscribe(
      result => {
        if (result) {
          alert('Product ' + inputProductName + ' added in database');
        }
        else {
          alert('Error adding product');
        }
      },
      error => {
        alert('Error adding product');
        console.error(error);
      })

    this.addProductForm.reset();
  }
}

export interface Container {
  containerCode: string;
  name: string;
}
