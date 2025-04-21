import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Livre } from '../models/livre.model';

@Injectable({
  providedIn: 'root'
})
export class LivreService {
  private apiUrl = 'https://localhost:5039/api/Livres';

  constructor(private http: HttpClient) { }

  getLivres(): Observable<Livre[]> {
    return this.http.get<Livre[]>(this.apiUrl);
  }

  getLivre(id: number): Observable<Livre> {
    return this.http.get<Livre>(`${this.apiUrl}/${id}`);
  }

  ajouterLivre(livre: Livre): Observable<Livre> {
    return this.http.post<Livre>(this.apiUrl, livre);
  }

  modifierLivre(livre: Livre): Observable<any> {
    return this.http.put(`${this.apiUrl}/${livre.id}`, livre);
  }

  supprimerLivre(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
