import * as types from './actionTypes';
import survey from './../fakedata/survey';
import { push, goBack } from 'react-router-redux';

export function getItem(item) {
    return {
        type: types.GET_ITEM,
        item
    };
}

export function deleteItem(id) {
    return {
        type: types.DELETE_ITEM,
        id
    };
}

export function setFilter(input) {
    return {
        type: types.SET_FILTER_STRING,
        input
    };
}

export function getItemDescriptions(items) {
    return {
        type: types.GET_ITEMS_DESCRIPTION,
        items
    };
}

export function setManageMode(payload) {
    return {
        type: types.SET_MANAGE_MODE,
        payload
    };
}

export function clearCurrentItem() {
    return {
        type: types.CLEAR_CURRENT_ITEM
    };
}

export const cancelCreation = (item) => {
    return function (dispatch) {
        dispatch(goBack());
        return dispatch(setManageMode(false));
    }
}

export const editItem = (item) => {
    return function (dispatch) {
        dispatch(push('/newsurvey'));
        dispatch(getItem(survey));
        return dispatch(setManageMode(true));
    }
}
export const addQuestion = (question)=>{
    return{
        type: types.ADD_QUESTION,
        question
    }
}

export function deleteQuestion(questionId){
    return {
        type: types.DELETE_QUESTION,
        questionId
    }
}

export function saveQuestion(question){
    return {
        type: types.SAVE_QUESTION,
        question
    }
}