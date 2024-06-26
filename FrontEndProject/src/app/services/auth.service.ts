import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authTokenKey: string = 'Authorization'
  
  setAuthToken(token: string): void{
    localStorage.setItem(this.authTokenKey, token);
  }

  getAuthToken(): string | null {
    return localStorage.getItem(this.authTokenKey);
  }

  hasAuthToken() : boolean {
    return localStorage.getItem(this.authTokenKey) !== null
  }

  removeAuthToken(): void{
    localStorage.removeItem(this.authTokenKey);
  }

}
