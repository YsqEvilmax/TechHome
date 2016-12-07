import {Component} from "angular2/core";
import {MyModel} from "./model";
import {RestApiService} from "./rest-api.service";

@Component({
    selector: `my-app`,
    templateUrl: "scripts/app.html"
})
export class MyApp {
    constructor(private apiService: RestApiService<string>) {
    }
    model = new MyModel();
    getValues() {
        return this.apiService.getAll();
    }
}