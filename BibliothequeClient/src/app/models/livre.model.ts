export interface Livre {
    id?: number;
    titre: string;
    auteur: string;
    isbn?: string;
    categorie?: string;
    annee?: number;
    description?: string;
    nombreExemplaires: number;
    exemplairesDisponibles: number;
}
