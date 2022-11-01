import { AbstractControl, ValidatorFn } from "@angular/forms";

export function IsValidName(): ValidatorFn {
    return (control: AbstractControl): any => {
        const value = <string>control.value;
        if (!value) return;
        if (value.length === 0) return;
        //ki tu dac biet va so
        const nameRegexp: RegExp = /[!@#$%^&*()_+\-=\[\]{};'`:"\\|,.<>\/?0-9]/;
        if(value && nameRegexp.test(value)){
            return { 
                IsValidName:{
                    message:'Name contains invalid characters'
                },
                IsValidAddress:{
                    message:'Address contains invalid characters'
                },
                IsValidTitle:{
                    message:'Title contains invalid characters'
                }
            }
        }
        return;
    }
}
export function IsValidAddress(): ValidatorFn {
    return (control: AbstractControl): any => {
        const value = <string>control.value;
        if (!value) return;
        if (value.length === 0) return;
        //ki tu dac biet 
        const nameRegexp: RegExp = /[!@#$%^&*()_+\-=\[\]{};'`:"\\|,.<>\?]/;
        if(value && nameRegexp.test(value)){
            return { 
                IsValidAddress:{
                    message:'Address contains invalid characters'
                }
            }
        }
        return;
    }
}

export function checkNonAlphanumericCharacter(): ValidatorFn {
    return (control: AbstractControl): any => {
        const value = <string>control.value;
        if (!value) return;
        if (value.length === 0) return;
        const nameRegexp: RegExp = /[!@#$%^&*()_+\-=\[\]{};'`:"\\|,.<>\?]/;
        const a = nameRegexp.test(value);
        if(value && a == false){
            return { 
                checkNonAlphanumericCharacter:{
                    message:'Password must have at least one non alphanumeric character.'
                }
            }
        }
        return;
    }
}