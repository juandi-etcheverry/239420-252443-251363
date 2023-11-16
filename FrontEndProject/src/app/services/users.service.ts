import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetUserResponse, SignupRequest, LoginRequest, UpdateUserProps, UpdateUserResponse, GetUsersResponse, CreateUserRequest, CreateUserResponse } from 'src/utils/interfaces';
import url from 'src/utils/url';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient, private _authService : AuthService) { }

  getUser(id: string){
    return this.http.get<GetUserResponse>(`${url}/users/${id}`);
  }

  getLoggedUser(){
    const auth = this._authService.getAuthToken()
    if (auth) {
      return this.http.get<GetUserResponse>(`${url}/sessions/user`, {headers: { Authorization : auth}});
    }
    return null
  }

  updateUser({id = '1' , email = '', address = '', role = 1}: UpdateUserProps){
    return this.http.put<UpdateUserResponse>(`${url}/users/${id}`, {Email: email, Address: address, Role: role});
  }

  signup({email, address, password, passwordConfirmation}: SignupRequest){
    return this.http.post(`${url}/signup`, {Email: email, Address: address, Password: password, PasswordConfirmation: passwordConfirmation},
    {observe: 'response'});
  }

  logout(){
    return this.http.post(`${url}/logout`, {});
  }

  login({email, password}: LoginRequest){
    return this.http.post(`${url}/login`, {Email: email, Password: password},
    {observe: 'response'});
  }

  deleteUser(id: string){
    return this.http.delete(`${url}/users/${id}`);
  }

  getAllUsers(): Observable<GetUsersResponse>{
    return this.http.get<GetUsersResponse>(`${url}/users`);
  }

  createUser(
    {
      email,
      address,
      role,
      password,
      passwordConfirmation
    }: CreateUserRequest): Observable<CreateUserResponse> {
    return this.http.post<CreateUserResponse>(`${url}/users`, 
      {Email: email, Address: address, Role: role, Password: password, PasswordConfirmation: passwordConfirmation});
  }
}
