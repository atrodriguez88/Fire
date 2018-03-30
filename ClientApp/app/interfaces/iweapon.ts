import { keyValuePair } from "./keyValuePair";

export interface Iweapon {

    id: number;
    model: keyValuePair;
    make: keyValuePair;
    features: keyValuePair[];
    contact: {
        name: string,
        email: string,
        phone: string
    };
    isRegistered: boolean;
    lastUpdate: string;
}

export interface ISaveWeapon {

    id: number;
    modelId: number;
    makeId: number;
    features: keyValuePair[];
    contact: {
        name: string,
        email: string,
        phone: string
    };
    isRegistered: boolean;
}
