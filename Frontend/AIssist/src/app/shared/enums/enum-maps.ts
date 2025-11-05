import { CriticalityTypes } from "./criticality-type.enum";
import { ProfileTypes } from "./profie-type.enum";

export const EnumTextMap = {
    ProfileTypes: {
      [ProfileTypes.Admin]: 'Admin',
      [ProfileTypes.Gerente]: 'Gerente',
      [ProfileTypes.Tecnico]: 'Técnico',
      [ProfileTypes.Comum]: 'Comum'
    },
    AccountType: {
      [CriticalityTypes.Baixo]: 'Baixo',
      [CriticalityTypes.Medio]: 'Médio',
      [CriticalityTypes.Alto]: 'Alto',
      [CriticalityTypes.Critico]: 'Crítico'
    }
  };
  