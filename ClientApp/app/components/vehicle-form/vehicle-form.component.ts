import { Component, OnInit } from "@angular/core";
import { VehicleService } from "../../services/vehicle.service";
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"]
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastyService: ToastyService
  ) {
    route.paramMap.subscribe(p => {
      if (p.get("id")) this.vehicle.id = p.get("id"); //+ convierte en numero
    });
  }

  ngOnInit() {
    if (this.vehicle.id > 0) {
      this.vehicleService.getVehicle(this.vehicle.id).subscribe(
        v => {
          this.vehicle = v;
        },
        err => {
          if (err.status == 404) this.router.navigate(["/home"]);
        }
      );
    }

    this.vehicleService.getMakes().subscribe(makes => {
      this.makes = makes;
    });

    this.vehicleService
      .getFeatures()
      .subscribe(features => (this.features = features));
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    //Si son muchos registros, acá debería llamar a un servicio, en vez de traer todos los modelos.
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    this.vehicleService.create(this.vehicle).subscribe(x => console.log(x));
  }
}
