export interface RootCause {
  id: number;
  rootCauseName: string;
  criticality: number;
  createdAt: string;   // ISO string do backend
  createdBy: string;
  updatedAt: string;   // ISO string do backend
  updatedBy: string;
  active: boolean;
  tickets: any | null; // pode ser um array de tickets ou null
}
