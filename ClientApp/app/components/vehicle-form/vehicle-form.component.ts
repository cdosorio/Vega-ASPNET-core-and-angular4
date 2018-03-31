import { Component, OnInit } from "@angular/core";
import { VehicleService } from "../../services/vehicle.service";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"]
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  vehicle: any = {};

  constructor(
    private vehicleService: VehicleService) {}

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => {
      this.makes = makes;
      //console.log("MAKES", this.makes);
    });

    this.vehicleService.getFeatures().subscribe(features => 
      this.features = features      
    );
  }

  onMakeChange(){
    
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
    //Si son muchos registros, acá debería llamar a un servicio, en vez de traer todos los modelos.
    this.models = selectedMake ? selectedMake.models : [];
  }
}
