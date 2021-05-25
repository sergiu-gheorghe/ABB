import './App.css';
import React, {useState, useEffect} from 'react'
import {BrowserRouter, Route, Switch} from 'react-router-dom'
import Users from './components/Users'
import Albums from './components/Albums'
import Photos from './components/Photos'
import Home from './components/Home'
import Navigation from './components/Navigation'

function App() {
  return (
    <BrowserRouter>
      <div className="container">

        <Navigation/>
        
        <Switch>
          <Route path='/' component={Home} exact/>
          <Route path='/users' component={Users}/>
          <Route path='/albums' component={Albums}/>
          <Route path='/photos' component={Photos}/>
        </Switch>
      </div>
    </BrowserRouter>
    
  );
}

export default App;
