import { Injectable }    from "angular2/core";
import { Headers, Http } from "angular2/http";
import "rxjs/add/operator/toPromise";

@Injectable()
export class RestApiService<T>{
    private headers = new Headers({ "Content-Type": "application/json" });
    public url: string = "http://localhost/api/values";

    public constructor(private http:Http) {
    }

    public get(id:number):Promise<T> {
        const url = `${this.url}/${id}`;
        return this.http
            .get(url, { headers: this.headers })
            .toPromise()
            .then(res => res.json().data as T)
            .catch(this.handleError);
    }

    public getAll(): Promise<T[]> {
        return this.http.get(this.url)
            .toPromise()
            .then(res => res.json().data as T[])
            .catch(this.handleError);
    }

    public update(id:number, t: T): Promise<T> {
        const url = `${this.url}/${id}`;
        return this.http
            .put(url, JSON.stringify(t), { headers: this.headers })
            .toPromise()
            .then(() => t as T)
            .catch(this.handleError);
    }

    public create(t: T): Promise<T> {
        return this.http
            .post(this.url, JSON.stringify(t), { headers: this.headers })
            .toPromise()
            .then(res => res.json().data as T)
            .catch(this.handleError);
    }

    public delete(id: number): Promise<void> {
        const url = `${this.url}/${id}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}