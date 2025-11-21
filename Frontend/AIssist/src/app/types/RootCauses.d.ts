export interface RootCause {
  id: number;
  rootCauseName: string;
  criticality: number;
  createdAt: string;
  createdBy: string;
  updatedAt: string;
  updatedBy: string;
  active: boolean;
  tickets: any | null;
}
