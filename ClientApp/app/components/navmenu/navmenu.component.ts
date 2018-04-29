import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: "nav-menu",
  templateUrl: "./navmenu.component.html",
  styleUrls: ["./navmenu.component.css"]
})
export class NavMenuComponent {
  constructor(private auth: AuthService) {
    auth.handleAuthentication(); // add this to your constructor
  }

  login(){
    this.auth.login();
  }

  salir(){
    this.auth.logout();
  }
}
