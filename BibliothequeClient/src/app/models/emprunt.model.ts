export interface Emprunt {
    id?: number;
    livreId: number;
    utilisateurId: number;
    dateEmprunt: Date;
    dateRetourPrevue: Date;
    dateRetourEffective?: Date;
    estRendu: boolean;
    commentaire?: string;
    livre?: any;
    utilisateur?: any;
}
