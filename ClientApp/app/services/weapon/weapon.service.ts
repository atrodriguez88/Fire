import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class WeaponService {

  urlBase: string = `http://localhost:5000`;

  constructor(private http: Http) { }

  getMakes(){

    const url = this.urlBase + `/api/makes`;

    return this.http.get(url)
      .map((res: any) => {
        console.log(res, `Service`);
        return res.json();
    })
  }

  getFeatures(){
    
    const url = this.urlBase + `/api/features`;

    return this.http.get(url)
      .map((res) => {
        return res.json();
    })

  }

}
