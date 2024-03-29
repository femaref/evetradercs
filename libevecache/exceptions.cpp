// libevecache - EVE Cache File Reader Library
// Copyright (C) 2009-2010  StackFoundry LLC and Yann Ramin
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// http://dev.eve-central.com/libevecache/
// http://gitorious.org/libevecache

#include "Stdafx.h"
#include "evecache/exceptions.hpp"

namespace EveCache {
    EndOfFileException::EndOfFileException()
    {
    }

    EndOfFileException::operator std::string () const
    {
        return std::string("End of file");
    }

    ParseException::ParseException(const std::string &m) : message(m)
    {
    }

    ParseException::ParseException(const char*m)
    {
        message = std::string(m);
    }

    ParseException::operator std::string () const
    {
        return message;
    }

};
