import { Pipe, PipeTransform } from "@angular/core";
import { EnumTextMap } from "../enums/enum-maps";

@Pipe({
    name: 'enumText',
    standalone: true
})
export class EnumTextPipe implements PipeTransform {
    transform(value: number, enumName: keyof typeof EnumTextMap): string {
        const enumMap = EnumTextMap[enumName];
        if (!enumMap) return 'Enum inválido';
        return (enumMap as Record<number, string>)[value] ?? 'Desconhecido';
    }
}