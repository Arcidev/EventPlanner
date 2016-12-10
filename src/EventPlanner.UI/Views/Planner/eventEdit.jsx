import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';
import {getBaseUrl} from './commonScript.jsx';

var center = { lat: 49.1951, lng: 16.6068 };
var zoom = 3;

const K_WIDTH = 40;
const K_HEIGHT = 40;

const greatPlaceStyle = {
    // initially any map object has left top corner at lat lng coordinates
    // it's on you to set object origin to 0,0 coordinates
    position: 'absolute',
    width: K_WIDTH,
    height: K_HEIGHT,
    left: -K_WIDTH / 2,
    top: -K_HEIGHT / 2,

    border: '5px solid #f44336',
    borderRadius: K_HEIGHT,
    backgroundColor: 'white',
    textAlign: 'center',
    color: '#3f51b5',
    fontSize: 16,
    fontWeight: 'bold',
    padding: 4
};

var myEvent = null;

class BasicInfoBlock extends React.Component {

    constructor(props) {
        super(props);

        this.state = { 
            name : "Event Name",
            desc : "Event Description"
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                name: response.data.name,
                desc: response.data.desc
            });
            myEvent = response.data;
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    handleSave(){
        myEvent.name = this.refs.eventName.value;
        myEvent.desc = this.refs.eventDesc.value;
        console.log(myEvent);
        axios
             .post(getBaseUrl()+`save`, myEvent)
             .catch(() => alert('Something went wrong :( '));
    }

    render(){
        return(
                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="eventName" className="col-sm-2 control-label">Name</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventName" ref="eventName" key={this.state.name} className="form-control" placeholder="Event name" defaultValue={this.state.name}/>
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="eventDesc" className="col-sm-2 control-label">Description</label>
                        <div className="col-sm-10">
                        <textarea id="eventDesc" ref="eventDesc" key={this.state.desc} className="form-control" rows="3" defaultValue={this.state.desc}></textarea>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                        <button type="submit" className="btn btn-default" onClick={this.handleSave.bind(this)} >Save event</button>
                        </div>
                    </div>
                </form>
            );
    }
}


class PeopleRows extends React.Component {
    constructor(props) {
        super(props);

        this.state = { 
            people : [],
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                people: response.data.people
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    handleAdd(){
        this.state.people.push("");
        this.forceUpdate();
    }


    render() {
        var rows = [];
        var count = 1;
        this.state.people.forEach(function (row)
        {
            var rowId = "personEmail" + count;
            rows.push
            (
                    <div className="form-group">
                        <label htmlFor={rowId} className="col-sm-2 control-label">Person's email</label>
                        <div className="col-sm-10">
                            <input type="email" id={rowId} className="form-control" defaultValue={row} />
                        </div>
                    </div>
            );
            count++;
        });

     
        return (   
            <div>
                <button type="button" className="btn btn-default" onClick={this.handleAdd.bind(this)}>Add people</button>
                <div>
                    {rows}
                </div>
            </div>
        );
    }
}

class PeopleBlock extends React.Component {
    render(){
        return(
            <form className="form-horizontal">             
                <PeopleRows/>
            </form>
        );
    }
}

class DateTimeRows extends React.Component{
    constructor(props) {
        super(props);

        this.state = { 
            dates : ["2016-01-01T10:10:00"],
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                dates: response.data.dates
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    render(){
        var rows = [];
        var count = 1;
        this.state.dates.forEach(function(date){
            var rowId = "eventDate" + count;
            rows.push
            (
                <div className="form-group">
                    <label htmlFor={rowId} className="col-sm-2 control-label">Datetime</label>
                    <div className="col-sm-10">
                    <input type="datetime-local" id={rowId} className="form-control" value={date}/>
                    </div>
                </div>
            );
            count++;
        })

        return(
                <div>
                    {rows}
                </div>
        );
    }
}

class DateTimeBlock extends React.Component {
    render(){
        return(
            <form className="form-horizontal">
                <button type="button" className="btn btn-default">Add date</button>
                <DateTimeRows/>
            </form>
        );
    }
}


class GoogleMapBlock extends React.Component{   
       
    constructor(props) {
        super(props);

        this.state = { 
            markers: []
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                markers: response.data.markers
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    render(){
     
        var myMarkers = [];
        var count = 1;
        this.state.markers.forEach(function(marker){
            var markerId = "markerId" + count;//marker.key
            myMarkers.push(
                <div id={markerId} style={greatPlaceStyle} lat={marker.position.lat} lng={marker.position.lng}>{marker.title}</div>
            );
            count++;
        });

        return(
                <div className="thumbnail ep-map">
                    <GoogleMap defaultCenter={center} defaultZoom={zoom}>
                    {myMarkers}
                    </GoogleMap>
                </div>
            );
    }
}

class PlaceBlock extends React.Component{
    render(){
        return(
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add place</button>
                    <div className="form-group">
                        <label htmlFor="eventPlace0" className="col-sm-2 control-label">Place</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventPlace0" className="form-control" />
                        </div>
                    </div>
                </form>        
        );
    }
}



class EventEditLayout extends React.Component {
    render() {

        let styles = {
                panel: {
                border: "solid #f1f1f1",
                borderWidth: "1 1px",
                borderRadius: "8px",

                padding: "15px"
                }
        }

        var mapUI = true;

        return (
            <div>

            <h2>Basic information</h2>
                <div style={styles.panel} >
                <BasicInfoBlock/>
                </div>
            <h2>People</h2>
                <div style={styles.panel} >
                <PeopleBlock/>
                </div>
            <h2>Date and Time</h2>
                <div style={styles.panel} >
                <DateTimeBlock/>
                </div>
            <h2>Place</h2>
                <div style={styles.panel} >
                <div>
                <GoogleMapBlock/>
                <PlaceBlock/>
                </div>                                 
                </div>
            </div>
        );
    }
}



const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}