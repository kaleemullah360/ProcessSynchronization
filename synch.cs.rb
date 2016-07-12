# coding: utf-8
require 'thread'
require "benchmark"

#####################

class Seat
  def initialize
    @seat_type = "empty"
    @animal_type = "none"
    @lock = Mutex.new
	end

  def seat_type=(seat_type)
    @seat_type = seat_type
	end

  def animal_type=(animal_type)
    @animal_type = animal_type
	end

  def seat_type
    @seat_type
	end

  def animal_type
    @animal_type
	end

  def available?
    @lock.locked?
	end

  def occupy!(type)
    return if seats_available.empty?
    @lock.synchronize {
      puts "Seat occupied by #{type}"
      @animal_type = type
      @seat_type = "full"
      eat()
      @animal_type = "none"
      @seat_type = "empty"
    }
	end

  def eat
    sleep(30)
  end
end

class Table
  def initialize(seats_count = 1)
		@seats = []
	end
  def create_seats(seats_count = 1)
    seats_count.times do
      @seats << Seat.new
    end
  end
  def seats
    @seats
  end
  def seats_available
    @seats.collect do |seat|
      seat if seat.available?
    end.compact
  end
  def no_shark
    !@seats.collect(&:animal_type).include?("shark")
  end
  def no_fish
    !@seats.collect(&:animal_type).include?("fish")
  end
  def fish_count
    @seats.collect(&:animal_type).count("fish")
  end
end

class Animal
  attr_accessor :status
  READY_TO_GO = 0
  SIT = 1
  EAT = 2
  WAIT = 3
  READY = 4
  FINISH = 5

  def initialize
    self.status = Animal::READY_TO_GO
  end
  def shark_fish_sync_cleanup
  end

  def eat_weed(number = 1)
    puts "#{self.class.to_s.downcase} no #{number} is eating weed this time\n"
    sleep_and_wake(number)
  end

  def sleep_and_wake(number)
    self.status = Animal::EAT
    sleep(1)
    self.status = Animal::FINISH
		puts "#{self.class.to_s.downcase} no #{number} is finishing the job\n"
  end

end

class Fish < Animal
  @@fish_number = 0
  attr_accessor :fish_no
  def initialize
    super
    self.fish_no = @@fish_number += 1
  end

  def fish_before_eat(table)
    if table.seats_available and table.no_shark
      eat_weed(fish_no)
    else
      #wait
    end
      self.status = Animal::WAIT
    end

  def fish_after_eat

  end
end

class Shark < Animal
  @@shark_number = 0
  attr_accessor :shark_no

  def initialize
    super
    self.shark_no = @@shark_number +=1
  end

  def shark_before_eat(table)
    puts table
    if table.seats_available and table.no_fish
      eat_weed(shark_no)
    elsif table.fish_count == 1
      fish = table.fishes.collect{|fish| fish.status == 'FINISH'}
      puts "#{self.class.to_s.downcase} no #{self.shark_no} is eating fish #{fish.fish_no} this time\n"
      table.fishes.delete(fish)
    elsif table.fish_count > 1
      self.status = Animal::WAIT
    end
  end

  def eat_weed(number)
    to_print = ["#{self.class.to_s.downcase} no #{number} is eating weed this time\n",
      "Yayy!! #{self.class.to_s.downcase} no #{number} is eating Fish from the table\n"]
    sleep_and_wake(to_print.sample)
  end

  def shark_after_eat(table)

  end
end

class Syncproblem
  def initialize(seats, sharks, fishes, iter)
    @table = Table.new
    @table.create_seats(seats)
    @sharks = []
    sharks.times do
      @sharks << Shark.new
    end
    @fishes = []
    fishes.times do
      @fishes << Fish.new
    end
  end
  def table
    @table
  end
  def sharks
    @sharks
  end
  def fishes
    @fishes
  end
end

#####################

class Semaphore
  def initialize(size = 1)
    @queue = SizedQueue.new(size)
    size.times { inc }
  end

  def inc
    tap { @queue.push(nil) }
  end
  alias increment inc
  alias up inc

  def dec
    tap { @queue.pop }
  end
  alias decrement dec
  alias down dec

  # @return [Integer]
  def size
    @queue.length
  end

  def synchronize
    decrement
    yield
  ensure
    increment
  end
end

if $0 == __FILE__
  # Example:
  puts "Enter the file name to enter"
  STDOUT.flush
  # file_name = gets.chomp
  # puts "File name is " + file_name
  # fname = "sample.txt"
  # somefile = File.open(fname, "w")
  # ["shark","fish","seats","iteration"].each do |input|
  #   puts "Enter #{input}"
  #   shark = gets.chomp
  #   somefile.puts shark
  # end
  # somefile.close

  problem = Syncproblem.new(2,3,10,1)
  sem = Semaphore.new(2)
  threads = []
  animals = problem.fishes + problem.sharks
  # animals = animals.shuffle!
  seats = problem.table.seats
  puts "Total seats: ", seats.length
  count = (problem.fishes + problem.sharks).size
  threads = Array.new(count) do |i|
    Thread.start do
      number = (animals[i].class == Fish) ? animals[i].fish_no : animals[i].shark_no
      puts "#{animals[i].class.to_s.downcase} no #{number} trying to enter…\n"
      sem.synchronize do
        time = Benchmark.realtime do
        puts "#{animals[i].class.to_s.downcase} has entered!\n"
         if animals[i].class.to_s.downcase == 'fish'
           # seats[i].animal_type = 'fish'
           # seats[i].occupy('fish')
           animals[i].eat_weed(number)
         end
        if animals[i].class.to_s.downcase == 'shark'
          animals[i].eat_weed(number)
        end
        sleep 1
        end
        puts "##{animals[i].class.to_s.downcase} no #{number} process took #{time} mm execution time \n"
      end
      sleep 1
      puts "#{animals[i].class.to_s.downcase} no #{number} has left!\n"
    end
  end
  threads.map(&:join)
  end

def time_method(method, *args)
  beginning_time = Time.now
  self.send(method, args)
  end_time = Time.now
  puts "Time elapsed #{(end_time - beginning_time)*1000} milliseconds"
end