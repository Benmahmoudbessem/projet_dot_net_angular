import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Utilisateur } from '../models/utilisateur.model';

@Injectable({
  providedIn: 'root'
})
export class UtilisateurService {
  private apiUrl = 'https://localhost:5039/api/Utilisateurs';

  constructor(private http: HttpClient) { }

  getUtilisateurs(): Observable<Utilisateur[]> {
    return this.http.get<Utilisateur[]>(this.apiUrl);
  }

  getUtilisateur(id: number): Observable<Utilisateur> {
    return this.http.get<Utilisateur>(`${this.apiUrl}/${id}`);
  }

  ajouterUtilisateur(utilisateur: Utilisateur): Observable<Utilisateur> {
    return this.http.post<Utilisateur>(this.apiUrl, utilisateur);
  }

  modifierUtilisateur(utilisateur: Utilisateur): Observable<any> {
    return this.http.put(`${this.apiUrl}/${utilisateur.id}`, utilisateur);
  }

  supprimerUtilisateur(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
