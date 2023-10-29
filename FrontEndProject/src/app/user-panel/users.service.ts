import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetUserResponse, UpdateUserResponse } from 'src/utils/interfaces';
import url from 'src/utils/url';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  getUser(id: string){
    return this.http.get<GetUserResponse>(`${url}/users/${id}`);
  }

  updateUser(id: string, email: string, address: string, role: number){
    return this.http.put<UpdateUserResponse>(`${url}/users/${id}`, {Email: email, Address: address, Role: role});
  }
}
