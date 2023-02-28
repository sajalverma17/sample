import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public containers: Container[] = [];
  public baseUrl: string;
  public http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;

    http.get<Container[]>(baseUrl + 'container/getwithproducts').subscribe(result => {
      this.containers = result;
    }, error => console.error(error));
  }

  // Remove the packaged product from the container on the UI. 
  public removeProduct(container: Container, product: Product): void {

    let containerIndex = this.containers.indexOf(container);

    // Selected container not found on UI, should never happen.
    if (containerIndex == -1) {
      alert('Error On the UI');
    }

    // Selected product not found in containers on UI, should never happen.
    const productIndex = this.containers[containerIndex].products.indexOf(product);
    if (productIndex == -1) {
      alert('Error On the UI');
    }

    // Remove the product from this container
    this.containers[containerIndex].products.splice(productIndex, 1);
    // alert('Deleted product ' + product.name + 'from container ' + container.displayname);
  }

  // Save the containers and the products listed in them
  public save(container: Container): void {
    let data = {
      "container": container,
    };
    this.http.put(this.baseUrl + 'container', data).subscribe(result => {
      if (result) {
        alert('Container ' + container.name + ' saved');
      }
      else {
        alert('Error On the UI');
      }
    }, error => console.error(error));
  }
}

interface Container {
  id: number,
  containerCode: string,
  name: string,
  products: Product[]
}

interface Product {
  id: number,
  name: string
}
