import {makeAutoObservable} from "mobx";
import ExerciseModel from "../models/ExerciseModel";
import setService from "../servise/set";
import SetModel from "../models/SetModel";
import exercise from "./exercise";


class Set {
    sets: SetModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    async getAllSet(id: string) {
        return setService.GetAllSet().then(res => {
            this.sets = res;
        });
    }

    async createNewSet(idExercise: string) {
        return setService.CreateNewSet(idExercise).then(res => {
            exercise.exercises.forEach(ex => ex.id === idExercise ? ex.sets.unshift(res) : null)
        });
    }

    async updateSet(id: string, repetitonNumber: number, setWeight: number) {
        return setService.UpdateSet(id, repetitonNumber, setWeight).then(res => {
            exercise.exercises.forEach(ex => {
                ex.sets.forEach(s => {
                    if (s.id === id) {
                        s = res
                    }
                })
            })
        });
    }

    async deleteSet(id: string) {
        return setService.DeleteSet(id).then(res => {
            exercise.exercises = exercise.exercises.map(ex => {
                return{
                    ...ex,
                    sets: [...ex.sets.filter(se => (se.id !== res.id ? res : se))]
                }
            })
        })
    };
}

const set = new Set();
export default set;
