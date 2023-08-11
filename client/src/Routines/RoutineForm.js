import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { addRoutine } from "../Managers/RoutineManager"
import { getAllPoses } from "../Managers/PoseManager"

export const RoutineForm = () => {
    const localSoulFlyUser = localStorage.getItem("users");
    const soulFlyUserObject = JSON.parse(localSoulFlyUser)
    const [poses, setPoses] = useState([])
    const navigate = useNavigate()

    const getPoses = () => {
        getAllPoses().then(allPoses => setPoses(allPoses));
    }

    useEffect(() => {
        getPoses()
    }, [])

    const [routine, update] = useState({
        intention: "",
        reflection:"",
        cycles: 0,
        poseId: "",
        reflection: "",
        creationDate: Date.now(),
        userId: soulFlyUserObject.id
    })
    

    const handleSaveButtonClick = (event) => {
        event.preventDefault()
        console.log("So you like to push buttons?")

        const routineToSendToAPI = {
            Intention: routine.intention,
            Reflection: routine.reflection,
            CreationDate: new Date().toISOString,
            Cycles: 0,
            PoseId: routine.poseId,
            UsersId: soulFlyUserObject.id
        }

        return addRoutine(routineToSendToAPI).then(navigate('/routines'))
        }

        const selectList = (event) => {
            const copy = {
                ...routine
            }
            copy.poseId = event.target.value
            update(copy)
        }  
        return (
            <div>
                <form className="routineForm">
                    <h2 className="routineForm">New Routine</h2>
    
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="pose-select">Poses</label>
                            {/* Select poses from the list */}
                            <select id="type"
                                value={
                                    routine.poseId
                                }
                                onChange={
                                    event => selectList(event)
                            }>
                                <option value="5">Select your poses</option>
                                {
                                poses.map(pose => {
                                    return <option value={pose.id} key={
                                        pose.id
                                    }>
                                        {
                                        pose.name
                                    }</option>
                            })
                            } </select>  
                            </div>
                    </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="intention">Intention</label>
                            <input id="intention" type="text" className="form-control"
                                value={
                                    routine.intention
                                }
                                onChange={
                                    (event) => {
                                        const copy = {
                                            ...routine
                                        }
                                        copy.intention = event.target.value
                                        update(copy)
                                    }
                                }/>
                        </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">
                        <label htmlFor="cycles">Cycles</label>
                        <input id="cycle" type="text" className="form-control"
                            value={
                                routine.cycles
                            }
                            onChange={
                                (event) => {
                                    const copy = {
                                        ...routine
                                    }
                                    copy.cycles = event.target.value
                                    update(copy)
                                }
                            }/>
                    </div>
            </fieldset>
    
    
        <button className="btn btn-primary"
            onClick={
                (clickEvent) => handleSaveButtonClick(clickEvent)
        }>
            Save Routine</button>
    </form></div>
        )
    
    
    }
