import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: 'vehicle-list.html'
})
export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[];
    allVehicles: Vehicle[]; //for the filter
    makes: KeyValuePair[];
    filter: any = {};

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        //Get makes
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);

        this.vehicleService.getVehicles()
            .subscribe(vehicles => this.vehicles = this.allVehicles = vehicles);
    }

    OnFilterChange() {
        var vehicles = this.allVehicles;

        if (this.filter.makeId)
            vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);

        this.vehicles = vehicles;
    }

    resetFilter() {
        this.filter = {};
        this.OnFilterChange();

    }

}