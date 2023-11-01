import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetUserResponse, SignupRequest, LoginRequest, UpdateUserProps, UpdateUserResponse } from 'src/utils/interfaces';
import url from 'src/utils/url';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  getUser(id: string){
    return this.http.get<GetUserResponse>(`${url}/users/${id}`);
  }

  updateUser({id = '1' , email = '', address = '', role = 1}: UpdateUserProps){
    return this.http.put<UpdateUserResponse>(`${url}/users/${id}`, {Email: email, Address: address, Role: role});
  }

  signup({email, address, password, passwordConfirmation}: SignupRequest){
    return this.http.post(`${url}/signup`, {Email: email, Address: address, Password: password, PasswordConfirmation: passwordConfirmation},
    {observe: 'response'});
  }
  login({email, password}: LoginRequest){
    return this.http.post(`${url}/login`, {Email: email, Password: password},
    {observe: 'response'});
  }
}
