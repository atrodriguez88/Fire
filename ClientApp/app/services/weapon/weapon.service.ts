import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class WeaponService {

  urlBase: string = `http://localhost:5000`;

  constructor(private http: Http) { }

  getMakes() {
    const url = this.urlBase + `/api/weapon/makes`;
    return this.http.get(url)
      .map((res: any) => {
        console.log(res, `Service`);
        return res.json();
      });
  }

  getFeatures() {
    const url = this.urlBase + `/api/features`;
    return this.http.get(url)
      .map((res) => {
        return res.json();
      });
  }
  
  getWeapon() {
    const url = this.urlBase + `/api/weapon`;
    return this.http.get(url)
      .map((res) => {
        return res.json();
      })
  }

  createWeapon(data: any) {
    const url = this.urlBase + `/api/weapon/create`;
    let body = data;
    return this.http.post('url', body)
      .map((res) => {
        return res.json();
      })
  }

  deleteWeapon(id: number) {
    const url = this.urlBase + `/api/weapon/delete`;
    return this.http.delete(url)
      .map((res) => {
        return res.json();
      })
  }

  updateWeapon(data: any) {
    const url = this.urlBase + `/api/weapon/update`;
    let body = data;
    return this.http.put(url,body)
      .map((res) => {
        return res.json();
      })
  }

}
