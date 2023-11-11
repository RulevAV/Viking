import {makeAutoObservable} from "mobx";
import ExerciseModel from "../models/ExerciseModel";
import exerciseService from "../servise/exercise";
import Workout from "./workout";


class Exercise {
    exercise: ExerciseModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    async createNewExercise(idWorkout: string, name: string) {
        return exerciseService.CreateNewExercise(idWorkout, name).then(res => {
            this.exercise.unshift(res);
            Workout.workouts = Workout.workouts.map(w => (res.idWorkout !== w.id ? w : {
                ...w,
                exercises: [...w.exercises, res]
            }));
        });
    }

    async updateExercise(id: string, exerciseName: string) {
        return exerciseService.UpdateExercise(id, exerciseName).then(res => {
            this.exercise = this.exercise.map(t => t.id === res.id ? res : t);
            Workout.workouts = Workout.workouts.map(w => {
                return res.idWorkout !== w.id ? w : {
                    ...w,
                    exercises: [...w.exercises.map(e => (e.id === res.id ? res : e))]
                }
            });
        });
    }

    async deleteExercise(id: string) {
        return exerciseService.DeleteExercise(id).then(res => {
            this.exercise = this.exercise.filter(t => t.id !== res.id);
            Workout.workouts = Workout.workouts.map(w=>{
                return {
                    ...w,
                    exercises: w.exercises.filter(e=> e.id !== res.id)
                }
            })
        });
    }
}

const exercise = new Exercise();
export default exercise;
