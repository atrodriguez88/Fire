import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { ISaveWeapon } from '../../interfaces/iweapon';

@Injectable()
export class WeaponService {

  urlBase: string = `http://localhost:5000`;

  constructor(private http: Http) { }

  getMakes() {
    const url = this.urlBase + `/api/weapon/makes`;
    return this.http.get(url)
      .map((res: any) => {
        // console.log(res, `Service`);
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

  getWeapon(id: any) {
    const url = this.urlBase + `/api/weapon`;
    return this.http.get(url + `/${id}`)
      .map((res) => {
        console.log(res, `Service`);
        return res.json();
      })
  }

  createWeapon(data: any) {
    const url = this.urlBase + `/api/weapon`;
    let body = data;
    return this.http.post(url, body)
      .map((res) => {
        return res.json();
      })
  }

  deleteWeapon(id: number) {
    const url = this.urlBase + `/api/weapon/` + id;
    return this.http.delete(url)
      .map((res) => {
        return res.json();
      })
  }

  updateWeapon(data: ISaveWeapon) {
    const url = this.urlBase + `/api/weapon/` + data.id;
    let body = data;
    return this.http.put(url, body)
      .map((res) => {
        return res.json();
      })
  }

  getWeapons(filters: any) {
    let query = this.queryString(filters);
    const url = this.urlBase + `/api/weapons` + '?' + query;

    console.log(url);
    return this.http.get(url)
      .map((res) => {
        return res.json();
      });
  }

  queryString(obj: any) {

    /*Best Solucion*/
    var part: any[] = [];

    for (const property in obj) {
      if (obj.hasOwnProperty(property)) {
        const value = obj[property];
        part.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }
    
    return part.join('&').toString();

    /* This one Way */

    // let query = ""
    // if (obj.makeId) {
    //   query += "MakeId=" + obj.makeId;
    // }
    // if (obj.modelId) {
    //   if (obj.makeId) {
    //     query += "&" + "ModelId=" + obj.modelId;
    //   }
    //   else {
    //     let query = "ModelId=" + obj.modelId;
    //   }
    // }
    // return query;
  }

}
