import {observer} from "mobx-react-lite";
import './workoutCard.css'
import React, {createRef, useEffect, useState} from 'react';
import {Button, Card, DatePicker, Input, InputRef, message, notification, Popconfirm} from 'antd';
import {EditFilled,DeleteFilled,PlusCircleFilled} from "@ant-design/icons";
import dayjs from "dayjs";
import workout from "../../state/workout";
import {Simulate} from "react-dom/test-utils";
import error = Simulate.error;



const tabList = [
    {
        key: 'tab1',
        tab: 'tab1',
    },
    {
        key: 'tab2',
        tab: 'tab2',
    },
];

const contentList: Record<string, React.ReactNode> = {
    tab1: <p>content1</p>,
    tab2: <p>content2</p>,
};

interface workoutPropsType {
    title: string,
    id:string
}

const Exercise: React.FC<workoutPropsType> = observer(({title,id}) => {
    const [activeTabKey1, setActiveTabKey1] = useState<string>('tab1');

    let data = new Date();
    let dateFormat = 'DD.MM.YYYY';

    const onTab1Change = (key: string) => {
        setActiveTabKey1(key);
    };

    const confirm = (e: React.MouseEvent<HTMLElement> | undefined) => {
        workout.deleteWorkout(id).then(t => {
            message.success('Успешно удалено');
        }).catch(e => {
            message.error('Ошибка')
        })
    };

    return (
        <Card
            style={{width: '100%'}}
            title={<div className='workoutCard-headerTitle'>
                {/*<span>{title}</span>*/}
                <Input
                    defaultValue={title}
                    onBlur={(e) => workout.updateWorkout(id,e.target.value)}
                ></Input>
                <span>
                        <DatePicker
                            disabled={true}
                            defaultValue={dayjs(data.toLocaleDateString('ru-RU'), dateFormat)}
                            format={dateFormat}/>
                    </span>
            </div>}
            tabList={tabList}
            extra={
                <div>
                    <Button size={"large"} icon={<PlusCircleFilled />} className='bg-success text-black-50' ></Button>
                    <Button size={"large"} icon={<EditFilled />} className='bg-info text-black-50' ></Button>
                    <Popconfirm
                        title="Удалить тренировку"
                        description="Уверены?"
                        onConfirm={confirm}
                        okText="Да"
                        cancelText="Нет"
                    >
                        <Button size={"large"}  icon={<DeleteFilled />} className='bg-danger text-black-50'></Button>
                    </Popconfirm>
                </div>
            }
            activeTabKey={activeTabKey1}
            onTabChange={onTab1Change}
        >
            {contentList[activeTabKey1]}
        </Card>
    );
});

export default Exercise;
