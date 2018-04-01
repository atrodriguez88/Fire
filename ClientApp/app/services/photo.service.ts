import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { resolve } from 'q';

@Injectable()
export class PhotoService {

    constructor(private http: Http) {

    }

    UploadPhoto(idWeapon: any, photo: File) {

        const url = "api/weapons/"
        var formData = new FormData();
        formData.append('file', photo)

        return this.http.post(`api/weapons/${idWeapon}/photos`, formData)
            .map((res) => {
                return res.json();
            });
    }

    // Usando js plain(AJAX) pork existen alunos problemas con los modulos de angular
    UpLoad(idWeapon: string, photo: File) {

        let promise = new Promise((res, rej) => {

            // I send by AJAX
            let fromData = new FormData();
            let xhr = new XMLHttpRequest();

            // File Data
            fromData.append('file', photo)
            // Peticion AJAX
            xhr.onreadystatechange = () => {

                // I used only this state when the peticion is UP, but I can used other states(EX: loading)
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        console.log('File UpLoad');
                        resolve(xhr.response);
                    } else {
                        console.log('File UpLoad Fail !!!!');
                        rej(xhr.response);
                    }
                }
            };

            let urlBase = `http://localhost:5000/`;  // Address Example
            let urlService = urlBase + `api/weapons/9/photos`;


            xhr.open(`POST`, urlService, true);
            xhr.send(fromData);
        });





    }

}