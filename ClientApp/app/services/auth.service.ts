// src/app/auth/auth.service.ts

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operator/filter';
import * as auth0 from 'auth0-js';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class AuthService {
  userProfile: any;
  private roles: string[] = [];

  auth0 = new auth0.WebAuth({
    clientID: 'OKFvE1I8Jz258LXblqRGRsPkpYS8V1CM',
    domain: 'cdosorio.auth0.com',
    responseType: 'token id_token',
    audience: 'https://cdosorio.auth0.com/userinfo',
    redirectUri: 'http://localhost:5000/vehicles',
    scope: 'openid profile email'
  });
//audience: 'https://api.vega.com',

  constructor(public router: Router) {
    this.userProfile = JSON.parse(localStorage.getItem('profile'));
    
    var token = localStorage.getItem('access_token');
    if (token){
      var jwtHelper = new JwtHelper();
      var decodedToken = jwtHelper.decodeToken(token);
      this.roles = decodedToken['http://vega.com/roles'];
    }
  }

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {    
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';

        this.getProfile(authResult.accessToken, (err,profile) => {
          
          localStorage.setItem('profile', JSON.stringify(this.userProfile));
          this.userProfile = profile;
        });

        this.setSession(authResult);        
        this.router.navigate(['/vehicles']);
      } else if (err) {
        this.router.navigate(['/home']);
        console.log(err);
      }
    });
  }

  private setSession(authResult): void {
    // Set the time that the Access Token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);

    var jwtHelper = new JwtHelper();
    var decodedToken = jwtHelper.decodeToken(authResult.accessToken);
    this.roles = decodedToken['http://vega.com/roles'];
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('profile');
    this.userProfile = null;
    this.roles = [];
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // Access Token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    return new Date().getTime() < expiresAt;
  }

  public getProfile(accessToken, cb): void {
    //const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      throw new Error('Access token must exist to fetch profile');
    }

    const self = this;
    this.auth0.client.userInfo(accessToken, (err, profile) => {
      if (profile) {
        self.userProfile = profile;
      }
      cb(err, profile);
    });
  }

  public isInRole(roleName){
    return this.roles.indexOf(roleName) > -1
  }

}